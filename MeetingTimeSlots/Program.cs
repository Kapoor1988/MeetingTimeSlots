using MeetingTimeSlots.Contracts;
using MeetingTimeSlot.Implementation;
using Microsoft.Extensions.DependencyInjection;
using MeetingTimeSlot.Contracts;

namespace MeetingTimeSlots
{
    class Program
    {
       
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services.AddSingleton<Executor, Executor>()
                .BuildServiceProvider()
                .GetService<Executor>()
                .Execute(args);

        
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IParticipantParser, ParticipantParser>();
            services.AddSingleton<ITimeSlots, TimeSlots>();
        }
    }

    
}
