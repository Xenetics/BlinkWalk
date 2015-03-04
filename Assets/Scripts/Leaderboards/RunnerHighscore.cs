using UnityEngine;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

public class RunnerHighscore
{
    public int id { get; set; }
    public string name { get; set; }
    public int time { get; set; }
    
    public RunnerHighscore()
	{}

    public RunnerHighscore(string name, int time)
	{
		this.name = name;
		this.time = time;
	}
	
	
	public override string ToString()
	{
		return "name: " + name + ", time: " + time;
	}
}
