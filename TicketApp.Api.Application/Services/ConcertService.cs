using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Application.Services;

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

    public async Task<Concert?> UpdateConcertAsync(Guid id, ConcertDto concertDto, CancellationToken cancellationToken)
    {
        var concert = await _concertRepository.GetConcertAsync(id, cancellationToken);
        var allowedContentTypes = new[] { "image/jpeg", "image/png" };

        if (concert != null)
        {
            concert.Name = concertDto.Name;
            concert.AvailableTicketAmount = concertDto.AvailableTicketAmount;
            concert.StartedAt = concertDto.StartedAt;
            concert.TicketPrice = concertDto.TicketPrice;

            if (concertDto.Image != null && concertDto.Image.Length != 0 && allowedContentTypes.Contains(concertDto.Image.ContentType))
            {
                try
                {
                    using var memoryStream = new MemoryStream();
                    await concertDto.Image.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();
                    concert.Image = imageData;
                }
                catch (Exception ex)
                {
                }
            }
            
            await _concertRepository.SaveChangesAsync(cancellationToken);
        }

        return concert;
    }

    public async Task<List<Concert>?> GetConcertsPageAsync(int page, int take, string? name, CancellationToken cancellationToken)
    {
        var lowerName = "";

        if (!string.IsNullOrWhiteSpace(name))
        {
            lowerName = name.ToLower();
        }

        var list = await _concertRepository.GetConcertsByName(lowerName, cancellationToken);

        return list.Skip((page - 1) * take).Take(take).ToList();
    }

    public async Task<List<Concert>?> GetConcertsByName(string name, CancellationToken cancellationToken)
    {
        return await _concertRepository.GetConcertsByName(name.ToLower(), cancellationToken);
    }

    public async Task<Guid> CreateConcert(ConcertDto concertDto, CancellationToken cancellationToken)
    {
        var allowedContentTypes = new[] { "image/jpeg", "image/png" };
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
        
        
        if (concertDto.Image != null && concertDto.Image.Length != 0 && allowedContentTypes.Contains(concertDto.Image.ContentType))
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await concertDto.Image.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();
                concert.Image = imageData;
            }
            catch (Exception ex)
            {
            }
        }

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