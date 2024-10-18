using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PROG6212.Models;

namespace PROG6212.Services
{
    public interface IClaimService
    {
        IEnumerable<Claim> GetPendingClaims();
        bool SubmitClaim(Claim claim, IFormFile supportingDocument);
        bool ApproveClaim(int claimId);
        bool RejectClaim(int claimId);
        bool FinalApproveClaim(int claimId);
        bool FinalRejectClaim(int claimId);
        IEnumerable<Claim> GetClaims(string statusFilter, DateTime? dateFilter);
        IEnumerable<Claim> GetLecturerClaims(string lecturerName); // Example method, adjust as needed
        IEnumerable<Claim> GetClaimsForManager(); // Example method, adjust as needed
    }
}