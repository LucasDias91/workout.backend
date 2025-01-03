using MediatR;

namespace Workout.Domain.Core.Pipeline
{
    public class MediatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        //private readonly LogManager _log;

        //public MediatorPipeline(LogManager log)
        //{
        //    _log = log;
        //}

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = next();

                //if (response.Exception != null)
                //   // _log.Save(response.Exception);

                return response;
            }
            catch (Exception ex)
            {//
                //_log.Save(ex);
                return null;
             
            }
        }


    }
}
