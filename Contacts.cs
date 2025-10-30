using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'contacts' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts 2D_STRING_ARRAY queries as parameter.
     */

    public static List<int> contacts(List<List<string>> queries)
    {
        List<int> result = new List<int>();
        Trie trie = new Trie();
        
        foreach (var query in queries) {
            string operation = query[0];
            string value = query[1];
            
            if (operation == "add") {
                trie.Insert(value);
            } else if (operation == "find") {
                int count = trie.CountPrefix(value);
                result.Add(count);
            }
        }
        
        return result;
    }
    
    public class TrieNode {
        public Dictionary<char, TrieNode> Children { get; set; }
        public int Count { get; set; }
        
        public TrieNode() {
            Children = new Dictionary<char, TrieNode>();
            Count = 0;
        }
    }
    
    public class Trie {
        private TrieNode root;
        
        public Trie() {
            root = new TrieNode();
        }
        
        public void Insert(string word) {
            TrieNode current = root;
            foreach (char c in word) {
                if (!current.Children.ContainsKey(c)) {
                    current.Children[c] = new TrieNode();
                }
                current = current.Children[c];
                current.Count++;
            }
        }
        
        public int CountPrefix(string prefix) {
            TrieNode current = root;
            foreach (char c in prefix) {
                if (!current.Children.ContainsKey(c)) {
                    return 0;
                }
                current = current.Children[c];
            }
            return current.Count;
        }
    }
    }

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int queriesRows = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<string>> queries = new List<List<string>>();

        for (int i = 0; i < queriesRows; i++)
        {
            queries.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
        }

        List<int> result = Result.contacts(queries);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
