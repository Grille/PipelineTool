using Grille.PipelineTool.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Grille.PipelineTool.WinForms.Tree;

public static class AssemblyTaskTypeTree
{

    static readonly NamespaceTreeNode _root;
    static readonly SortedDictionary<string, TypeTreeNode> _types;

    public static bool Initialized { get; private set; }
    public static NamespaceTreeNode Root => _root;

    static AssemblyTaskTypeTree()
    {
        _types = new SortedDictionary<string, TypeTreeNode>();
        _root = new NamespaceTreeNode("ROOT");

        AddType<NopTask>("Comment");

        LoadAssembly(typeof(AssemblyTaskTypeTree).Assembly);
    }

    private static void AddType(Type type, PipelineTaskAttribute attr, DuplicateHandling duplicateHandling)
    {
        var key = attr.Key;
        var node = new TypeTreeNode(type, attr);

        switch (duplicateHandling)
        {
            case DuplicateHandling.Ignore:
                if (!_types.ContainsKey(key)) _types[key] = node;
                break;
            case DuplicateHandling.Throw:
                _types.Add(key, node);
                break;
            case DuplicateHandling.Overwrite:
                _types[key] = node;
                break;
        }
    }

    public static void AddType<T>(string key) where T : PipelineTask
    {
        var type = typeof(T);
        var node = new TypeTreeNode(type, key);
        _types.Add(key, node);
    }

    static public void ApplyTo(TreeNodeCollection collection)
    {
        foreach (var node in Root.Nodes)
        {
            collection.Add((TreeNode)node);
        }
    }

    public static bool TryGetTypeInfo(Type? type, [MaybeNullWhen(false)] out TypeTreeNode node)
    {
        if (type == null)
        {
            node = null;
            return false;
        }

        foreach (var typeInfo in _types.Values)
        {
            if (typeInfo.Type == type)
            {
                node = typeInfo;
                return true;
            }
        }

        node = null;
        return false;
    }

    public static TypeTreeNode GetTypeInfo(Type type)
    {
        if (TryGetTypeInfo(type, out var node))
        {
            return node;
        }
        throw new KeyNotFoundException();
    }

    public enum DuplicateHandling
    {
        Throw,
        Ignore,
        Overwrite,
    }

    public static void LoadAssembly(Assembly assembly, DuplicateHandling duplicateHandling = DuplicateHandling.Throw)
    {
        var asmTypes = assembly.GetTypes();

        foreach (var type in asmTypes)
        {
            var attr = type.GetCustomAttribute<PipelineTaskAttribute>();
            if (attr == null)
                continue;

            if (!attr.Visible)
                continue;

            AddType(type, attr, duplicateHandling);
        }
    }

    public static void Initialize()
    {
        if (Initialized)
            throw new InvalidOperationException();

        //_types.Sort((a, b) => a.Key.CompareTo(b.Key));

        foreach (var typeInfo in _types.Values)
        {
            string[] path = typeInfo.NamespacePath;
            var currentNode = _root;

            foreach (string segment in path)
            {
                if (!currentNode.Groups.TryGetValue(segment, out var childNode))
                {
                    childNode = new NamespaceTreeNode(segment);
                    currentNode.Groups.Add(segment, childNode);
                }

                currentNode = childNode;
            }

            if (currentNode.Values.ContainsKey(typeInfo.Name))
            {
                throw new Exception();
            }
            currentNode.Values[typeInfo.Name] = typeInfo;
        }

        _root.Init();

        Initialized = true;
    }
}
