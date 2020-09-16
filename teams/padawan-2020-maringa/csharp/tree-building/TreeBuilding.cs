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
    public List<Tree> Children { get; } = new List<Tree>();
    public bool IsLeaf => Children.Count == 0;

}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        if (!(records?.Any() ?? false))
            throw new ArgumentException();

        records = records.OrderBy(record => record.RecordId).ToList();

        var hasInvalidItems = records.Where((r, index) =>
            r.RecordId != index ||
            (r.RecordId == 0 && r.ParentId != 0) ||
            (r.RecordId != 0 && r.ParentId >= r.RecordId)).Any();

        if (hasInvalidItems)
            throw new ArgumentException();

        var trees = records.Select((record) => new Tree(record)).ToList();
        trees.ForEach(item =>
        {
            if (item.RecordId > 0)
                trees.First(record => record.RecordId == item.ParentId).Children.Add(item);
        });
        return trees.First();
    }
}