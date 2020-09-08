using System;
using System.Collections.Generic;
using System.Linq;

public class HighScores
{

    public List<int> ScoreList { get; }

    public HighScores(List<int> list) => ScoreList = list;

    public List<int> Scores() => ScoreList;

    public int Latest() => ScoreList.Last();

    public int PersonalBest()
    {
        ScoreList.OrderByDescending(q => q);
        return ScoreList[0];
    }

    public List<int> PersonalTopThree()
    {
        throw new NotImplementedException();
    }
}