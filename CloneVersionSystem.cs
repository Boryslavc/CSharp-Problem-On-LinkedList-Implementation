using System;
using System.Collections.Generic;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
	private List<Clone> clones = new List<Clone>();

    public CloneVersionSystem()
    {
        clones.Add(new Clone(1));
    }

	public string Execute(string query)
	{
        var split = query.Split(' ');

        switch(split[0])
        {
            case "learn":
                return LearnCommand(Convert.ToInt32(split[1]), split[2]);

            case "rollback":
                return RollbackCommand(Convert.ToInt32(split[1]));

            case "check":
                return CheckCommand(Convert.ToInt32(split[1]));

            case "relearn":
                return RelearnCommand(Convert.ToInt32(split[1]));

            case "clone":
                return CloneCommand(Convert.ToInt32(split[1]));

            default:
                return null;
        }
	}

    private string CloneCommand(int id)
    {
        var newClone = new Clone(clones[id - 1], clones.Count + 1);
        clones.Add(newClone);
        return null;
    }

    private string RelearnCommand(int cloneId)
    {
        clones[cloneId - 1].Relearn();
        return null;
    }

    private string CheckCommand(int cloneId)
    {
        return clones[cloneId - 1].Check();
    }

    private string LearnCommand(int cloneId, string command)
    {
        clones[cloneId - 1].LearnProgram(command);
        return null;
    }

    private string RollbackCommand(int cloneId)
    {
        clones[cloneId - 1].Rollback();
        return null;
    }


    private class Clone
    {
        public readonly int Id;

        private LinkedStack<string> programs;
        private LinkedStack<string> forgottenPrograms;

        private HashSet<string> learned; 

        public Clone(int id)
        {
            Id = id;
            programs = new LinkedStack<string>();
            programs.Push("basic");

            forgottenPrograms = new LinkedStack<string>();
            learned = new HashSet<string>();
        }

        public Clone(Clone clone, int id)
        {
            this.Id = id;

            this.learned = new HashSet<string>(clone.learned);

            this.programs = new LinkedStack<string>(clone.programs);
            this.forgottenPrograms = new LinkedStack<string>(clone.forgottenPrograms);
        }

        public void LearnProgram(string program)
        {
            // no hashset - we sacrifice time complexity but spare space complexity
            // with hashset - we sacrifice space complexity, but safe time on lookup
            // cannot add hashset to linkedStack class since it destroys the idea of a stack
            if (!learned.Contains(program))
            {
                programs.Push(program);
                learned.Add(program);
            }
        }

        public void Rollback()
        {
            if (programs.Count > 1)
            {
                var programToRemove = programs.Pop();
                learned.Remove(programToRemove);
                forgottenPrograms.Push(programToRemove);
            }
        }

        public void Relearn()
        {
            if (forgottenPrograms.Count > 0)
            {
                var relearnProgram = forgottenPrograms.Pop();
                learned.Add(relearnProgram);
                programs.Push(relearnProgram);
            }
        }

        public string Check()
        {
            if (programs.Count > 0)
            {
                var res = programs.Pop();
                learned.Remove(res);
                return res;
            }
            return null;
        }
    }
}