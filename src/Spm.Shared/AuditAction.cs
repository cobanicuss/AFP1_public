namespace Spm.Shared
{
    public enum AuditAction
    {
        ExternalTriggerReceived = 5,
        RequestReceivedFromServiceForSoap = 10,
        ResponseReceivedFromServiceForSoap = 20,
        RequestReceived = 30,
        ResponseReceivedFromSap = 40,
        SendRequestToSap = 50,
        SendResponseToSap =60,
        SendToSaga = 70,
        MessageImplemented = 80,
        NewSagaStarted = 90,
        SagaCompleted = 100,
        SagaRetryLimitReached = 110,
        SagaNoResponseFromSap = 120,
        SagaSendToServiceForSoap = 130,
        SagaReTryToServiceForSoap = 140,
        SagaReceivedCommand = 150,
        ErrorImplementingMessage = 160,
        DataExtractionComplete = 170,
        FileWatcherInit = 180
    }
}