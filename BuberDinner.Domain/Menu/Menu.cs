using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.Menu;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();
    public string Name { get; }
    public string Description { get; }
    public AverageRating AverageRating { get; }
    public HostId HostId { get; }
    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
   
    public DateTime CreateDateTime { get; }
    public DateTime UpdateDateTime { get; }

    private Menu(
        MenuId id,
        List<MenuSection> sections,
        string name,
        string description,
        HostId hostId,
        AverageRating averageRating,
        DateTime createDateTime,
        DateTime updateDateTime) 
        : base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        _sections = sections;
        AverageRating = averageRating;
        CreateDateTime = createDateTime;
        UpdateDateTime = updateDateTime;
    }

    public static Menu CreateUnique(
        string name,
        string description,
        HostId hostId,
        List<MenuSection>? sections = null)
    {
        return new(
            MenuId.CreateUnique(),
            sections ?? new(),
            name,
            description,
            hostId,
            AverageRating.CreateNew(),
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
