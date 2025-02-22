using Blog.Domain.Entities;
using Blog.Domain.Models.JobHistory;

namespace Blog.Application.Abstract;

public interface IJobHistoryService
{
    Task<bool> AddJobHistory(CreateJobHistoryModel jobHistory);
    Task<bool> UpdateJobHistory(UpdateJobHistoryModel jobHistory);
    Task<bool> DeleteJobHistory(int id);
    Task<JobHistory> GetJobHistory(int id);
    Task<List<JobHistory>> GetJobHistories();
}
