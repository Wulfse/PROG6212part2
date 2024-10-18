using PROG6212.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PROG6212.Data;
namespace PROG6212.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Claim> GetPendingClaims()
        {
            return _context.Claims.Where(c => c.Status == "Pending").ToList();
        }

        public bool SubmitClaim(Claim claim, IFormFile supportingDocument)
        {
            try
            {
                if (supportingDocument != null && supportingDocument.Length > 0)
                {
                    var filePath = Path.Combine("uploads", supportingDocument.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        supportingDocument.CopyTo(stream);
                    }
                    claim.SupportingDocumentFilePath = filePath;
                }

                _context.Claims.Add(claim);
                _context.SaveChanges();
                return true; // Indicate success
            }
            catch
            {
                return false; // Indicate failure
            }
        }

        public bool ApproveClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimId);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Approved";
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RejectClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimId);
            if (claim != null && claim.Status == "Pending")
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool FinalApproveClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimId);
            if (claim != null && claim.Status == "Approved")
            {
                claim.Status = "Final Approved";
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool FinalRejectClaim(int claimId)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimId);
            if (claim != null && claim.Status == "Approved")
            {
                claim.Status = "Final Rejected";
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Claim> GetClaims(string statusFilter, DateTime? dateFilter)
        {
            var query = _context.Claims.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(c => c.Status == statusFilter);
            }

            if (dateFilter.HasValue)
            {
                query = query.Where(c => c.SubmissionDate.Date == dateFilter.Value.Date);
            }

            return query.ToList();
        }

        // Placeholder for GetLecturerClaims method
        public IEnumerable<Claim> GetLecturerClaims(string lecturerName)
        {
            return _context.Claims.Where(c => c.LecturerName == lecturerName).ToList();
        }

        // Placeholder for GetClaimsForManager method
        public IEnumerable<Claim> GetClaimsForManager()
        {
            return _context.Claims.Where(c => c.Status == "Approved").ToList(); // Adjust as necessary
        }
    }
}