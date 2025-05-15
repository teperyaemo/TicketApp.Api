using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Appication.Services;

public class ConcertService : IConcertService
{
    private readonly IConcertRepository _concertRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserContext _userContext;

    public ConcertService(ITicketRepository ticketRepository, IConcertRepository concertRepository,
        IUserContext userContext)
    {
        _ticketRepository = ticketRepository;
        _concertRepository = concertRepository;
        _userContext = userContext;
    }

    public async Task<Guid> BuyTicketAsync(Guid id, CancellationToken cancellationToken)
    {
        var concert = await _concertRepository.GetConcertAsync(id, cancellationToken);
        concert.SoldTickets++;
        concert.AvailableTicketAmount--;

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            UserId = _userContext.UserId,
            ConcertId = id
        };

        await _ticketRepository.CreateTicket(ticket, cancellationToken);

        await _ticketRepository.SaveChangesAsync(cancellationToken);

        return ticket.Id;
    }

    public async Task<Concert?> GetConcertAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _concertRepository.GetConcertAsync(id, cancellationToken);
    }

    public async Task<Concert?> UpdateConcertAsync(ConcertUpdateDto concertDto, CancellationToken cancellationToken)
    {
        var concert = await _concertRepository.GetConcertAsync(concertDto.Id, cancellationToken);

        if (concert == null)
            return null;

        concert.Name = concertDto.Name;
        concert.AvailableTicketAmount = concertDto.AvailableTicketAmount;
        concert.StartedAt = concertDto.StartedAt;

        await _concertRepository.SaveChangesAsync(cancellationToken);

        return concert;
    }

    public async Task<List<Concert>?> GetConcertsPageAsync(int page, int take, CancellationToken cancellationToken)
    {
        return await _concertRepository.GetConcertsPageAsync((page - 1) * take, take, cancellationToken);
    }

    public async Task<List<Concert>?> GetConcertsByName(string name, CancellationToken cancellationToken)
    {
        return await _concertRepository.GetConcertsByName(name.ToLower(), cancellationToken);
    }

    public async Task<Guid> CreateConcert(ConcertDto concertDto, CancellationToken cancellationToken)
    {
        var concert = new Concert
        {
            Id = default,
            Name = concertDto.Name,
            StartedAt = concertDto.StartedAt,
            AvailableTicketAmount = concertDto.AvailableTicketAmount,
            SoldTickets = 0,
            TicketPrice = concertDto.TicketPrice,
            CreatedAt = DateTimeOffset.Now,
            Tickets = null
        };

        await _concertRepository.CreateConcertAsync(concert, cancellationToken);
        await _concertRepository.SaveChangesAsync(cancellationToken);

        return concert.Id;
    }

    public async Task DeleteConcert(Guid id, CancellationToken cancellationToken)
    {
        var concert = await _concertRepository.GetConcertAsync(id, cancellationToken);

        if (concert != null)
            _concertRepository.DeleteConcert(concert);

        await _concertRepository.SaveChangesAsync(cancellationToken);
    }
}