using System;
using System.Collections.Generic;
using System.Linq;
public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}
public class Tree : TreeBuildingRecord
{
    public Tree(TreeBuildingRecord record)
    {
        ParentId = record.ParentId;
        RecordId = record.RecordId;
    }

    public List<Tree> Children {get;} = new List<Tree>();
    public bool IsLeaf => Children.Count == 0;

}
public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        if(!records.Any())
            throw new ArgumentException();

        records = records.OrderBy(q => q.RecordId).ToList();

        var recordListWhere = records.Where((r, index) => 
             r.RecordId != index || 
            (r.RecordId == 0 && r.ParentId != 0) || 
            (r.RecordId != 0 && r.ParentId >= r.RecordId)).Any();

        if (recordListWhere) 
            throw new ArgumentException();

        var trees = new List<Tree>();
        foreach (var record in records)
        {
            var t = new Tree(record);

            if (record.RecordId != 0)
                trees.First(i => i.RecordId == t.ParentId).Children.Add(t);

            trees.Add(t);
        }
        return trees.First(t => t.RecordId == 0);
    }
}