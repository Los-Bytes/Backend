using System.Collections;

namespace Backend.API.Laboratories.Domain.Model.ValueObjects;

/// <summary>
///     Member List Value Object
/// </summary>
public record MemberList : IEnumerable<int>
{
    private readonly List<int> _members;

    public int Count => _members.Count;

    public MemberList() : this(new List<int>())
    {
    }

    public MemberList(IEnumerable<int> members)
    {
        _members = members?.Where(m => m > 0).Distinct().ToList() ?? new List<int>();
    }

    public MemberList Add(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("User ID must be positive", nameof(userId));

        if (_members.Contains(userId))
            return this;

        var newMembers = new List<int>(_members) { userId };
        return new MemberList(newMembers);
    }

    public MemberList Remove(int userId)
    {
        var newMembers = _members.Where(m => m != userId).ToList();
        return new MemberList(newMembers);
    }

    public bool Contains(int userId) => _members.Contains(userId);

    public List<int> ToList() => new(_members);

    public IEnumerator<int> GetEnumerator() => _members.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator List<int>(MemberList memberList) => memberList.ToList();
    public static explicit operator MemberList(List<int> members) => new(members);
}