﻿using Application.Interfaces;
using Domain;

namespace Application.Companies
{
    public interface IDetails
    {
        Task<Company?> ExecuteAsync(string companyId);
    }

    public class Details : IDetails
    {
        private readonly ICompanyRepository _companyRepository;

        public Details(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company?> ExecuteAsync(string companyId)
        {
            return await _companyRepository.GetCompanyByIdAsync(companyId);
        }
    }
}
