using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Tags : MonoBehaviour
{
    [SerializeField] 
    private List<Tag> _tags;

    public List<Tag> All => _tags;

    public bool HasTag(Tag t)
    {
        return _tags.Contains(t);
    }

    public bool HasTag(string tagName)
    {
        return _tags.Exists(t => t.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase));
    }
}
