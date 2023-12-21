namespace Helpers.AStarFun;

public interface ISearchProblem<T>
{
	T Start();
	bool IsGoal(T node);
	IEnumerable<(int edgeCost, T child)> Expand(T node);
	int Heuristic(T node);
}

public class AStar<T> where T : notnull
{
	ISearchProblem<T> _problem;
	PriorityQueue<(T, int, T), int> _queue = new();
	Dictionary<T, T> _parent = new();
	T? _goal;

	public AStar(ISearchProblem<T> sp)
	{
		_problem = sp;
	}

	public List<T> GetPath()
	{
		if(_goal is null)
			throw new NotSupportedException("Run search first dammit");

		var res = new List<T>([_goal]);
		var current = _goal;
		while(_parent.TryGetValue(current, out var next))
		{
			res.Add(next);
			current = next;
		}

		res.Reverse();

		return res;
	}

	public void Search()
	{
		var node = _problem.Start();
		EnqueueChildren(node, 1);

		while(!_problem.IsGoal(node))
		{
			(node, int cost, var source) = _queue.Dequeue();
			if(_parent.ContainsKey(node))
				continue;

			_parent[node] = source;

			EnqueueChildren(node, cost);
		}

		_goal = node;
	}

	private void EnqueueChildren(T node, int nodeCost)
	{
		var children = _problem.Expand(node);
		foreach(var (edge, child) in children)
		{
			int h = _problem.Heuristic(child);
			int cost = nodeCost + edge;
			var weight = h + cost;

			_queue.Enqueue((child, cost, node), weight);
		}
	}
}
