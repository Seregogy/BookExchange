using System;
using System.Collections.Generic;

namespace Store.Model;

public class Product : ICloneable
{
    private int id;

    private string name;
    private string description;
    private string category;
    private double cost;
    private float rating;

    private string provider;
    
    private List<string> tags;
    private List<Commentary> commentaries;

    private List<Spec> specs;
    private List<string> pictures;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string Category { get => category; set => category = value; }
    public double Cost { get => cost; set => cost = value; }
    public float Rating { get => rating; set => rating = value; }

    public string Provider { get => provider; set => provider = value; }

    public List<string> Tags { get => tags; set => tags = value; }
    public List<Commentary> Commentaries { get => commentaries; set => commentaries = value; }
    
    public List<Spec> Specs { get => specs; set => specs = value; }
    public List<string> Pictures { get => pictures; set => pictures = value; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override bool Equals(object? obj) =>
        obj is Product other && other.Id == Id;

    public override int GetHashCode() => Id;
}