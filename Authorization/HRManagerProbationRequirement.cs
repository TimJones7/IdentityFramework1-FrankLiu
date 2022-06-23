using Microsoft.AspNetCore.Authorization;

namespace IdentityFramework1_FrankLiu.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public HRManagerProbationRequirement(int probabtionMonths)
        {
            ProbabtionMonths = probabtionMonths;
        }

        public int ProbabtionMonths { get; }



        
    }





    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if(!context.User.HasClaim(x => x.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }

            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;

            if(period.Days > 30 * requirement.ProbabtionMonths)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

            

        }
    }


}
