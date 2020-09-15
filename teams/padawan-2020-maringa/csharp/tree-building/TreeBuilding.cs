﻿using System;
using System.Collections.Generic;
using System.Linq;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Tree> Children { get; set; }
    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        records = records.OrderBy(q => q.RecordId);

        var recordsList = records.ToList();
        var recordListWhere = recordsList.Where((record, indexOfRecord) => record.RecordId != indexOfRecord).Any();
        var recordListAny = recordsList.Any(record =>
            (record.RecordId == 0 && record.ParentId != 0) ||
            (record.RecordId != 0 && record.ParentId >= record.RecordId));

        if (recordListAny || !recordsList.Any() || recordListWhere)
            throw new ArgumentException();

        var trees = new List<Tree>();
        foreach (var record in recordsList)
        {
            var t = new Tree
            {
                Children = new List<Tree>(),
                Id = record.RecordId,
                ParentId = record.ParentId
            };

            if (record.RecordId != 0)
                trees.First(i => i.Id == i.ParentId).Children.Add(t);

            trees.Add(t);
        }
        return trees.First(t => t.Id == 0);
    }
}