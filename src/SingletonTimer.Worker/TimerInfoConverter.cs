using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Converters;
using Microsoft.Azure.Functions.Worker.Core;
using Microsoft.Azure.Functions.Worker.Extensions.Abstractions;
using System;
using System.Threading.Tasks;
using TimerInfo = Common.TimerInfo;
using ScheduleStatus = Common.ScheduleStatus;

namespace SingletonTimer.Worker
{
    [SupportsDeferredBinding]
    [SupportedTargetType(typeof(TimerInfo))]
    internal class TimerInfoConverter : IInputConverter
    {
        public ValueTask<ConversionResult> ConvertAsync(ConverterContext context)
        {
            try
            {
                ConversionResult result = context?.Source switch
                {
                    ModelBindingData binding => ConversionResult.Success(ConvertToTimerInfo(binding)),
                    _ => ConversionResult.Unhandled()
                };

                return new ValueTask<ConversionResult>(result);
            }
            catch (Exception exception)
            {
                return new ValueTask<ConversionResult>(ConversionResult.Failed(exception));
            }
        }

        private TimerInfo ConvertToTimerInfo(ModelBindingData binding)
        {
            if (binding is null)
            {
                throw new ArgumentNullException(nameof(binding));
            }

            // test
            return new TimerInfo(new ScheduleStatus(), false);
        }
    }
}
