using AutoMapper;
using Blog.Application.Abstract;
using Blog.Domain.Context;
using Blog.Domain.Entities;
using Blog.Domain.Models.JobHistory;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistance.Concrete;

public class JobHistoryService : IJobHistoryService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public JobHistoryService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<bool> AddJobHistory(CreateJobHistoryModel jobHistory)
    {
        var ent = _mapper.Map<JobHistory>(jobHistory);
        var res = await _appDbContext.JobHistories.AddAsync(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteJobHistory(int id)
    {
        var res = await _appDbContext.JobHistories.FindAsync(id);
        if (res != null)
        {
            _appDbContext.JobHistories.Remove(res);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<JobHistory>> GetJobHistories()
    {
        var res = await _appDbContext.JobHistories.OrderByDescending(aa => aa.StartDate).ToListAsync();
        return res;
    }

    public async Task<JobHistory> GetJobHistory(int id)
    {
        var res = await _appDbContext.JobHistories.FindAsync(id);
        return res;
    }

    public async Task<bool> UpdateJobHistory(UpdateJobHistoryModel jobHistory)
    {
        var ent = _mapper.Map<JobHistory>(jobHistory);
        var res = _appDbContext.JobHistories.Update(ent);
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}
