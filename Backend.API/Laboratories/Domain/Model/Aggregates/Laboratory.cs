using Backend.API.Laboratories.Domain.Model.Commands;
using Backend.API.Laboratories.Domain.Model.ValueObjects;

namespace Backend.API.Laboratories.Domain.Model.Aggregates;

/// <summary>
///     Laboratory Aggregate Root
/// </summary>
public partial class Laboratory
{
    public Laboratory()
    {
        Name = new LaboratoryName("Default Laboratory");
        Address = Address.FromFullAddress("Unknown Address");
        Phone = new PhoneNumber("+1234567890");
        Capacity = new Capacity(1);
        RegistrationDate = DateTime.UtcNow;
        Members = new MemberList();
    }

    public Laboratory(CreateLaboratoryCommand command)
    {
        Name = new LaboratoryName(command.Name);
        Address = Address.FromFullAddress(command.Address);
        Phone = new PhoneNumber(command.Phone);
        Capacity = new Capacity(command.Capacity);
        RegistrationDate = command.RegistrationDate;
        LabResponsibleId = command.LabResponsibleId;
        AdminUserId = command.AdminUserId;
        Members = new MemberList(command.MemberUserIds ?? new List<int>());
    }

    public int Id { get; }
    public LaboratoryName Name { get; private set; }
    public Address Address { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public Capacity Capacity { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public int? LabResponsibleId { get; private set; }
    public int AdminUserId { get; private set; }
    public MemberList Members { get; private set; }

    public void Update(string name, string address, string phone, int capacity)
    {
        Name = new LaboratoryName(name);
        Address = Address.FromFullAddress(address);
        Phone = new PhoneNumber(phone);
        Capacity = new Capacity(capacity);
    }

    public void AddMember(int userId)
    {
        if (userId == AdminUserId)
            throw new InvalidOperationException("Admin is already part of the laboratory");

        Members = Members.Add(userId);
    }

    public void RemoveMember(int userId)
    {
        if (userId == AdminUserId)
            throw new InvalidOperationException("Cannot remove admin from laboratory");

        Members = Members.Remove(userId);
    }

    public bool IsAdmin(int userId) => AdminUserId == userId;
    public bool IsMember(int userId) => Members.Contains(userId);
    public bool HasAccess(int userId) => IsAdmin(userId) || IsMember(userId);
    public int TotalMembers => Members.Count + 1;
}
