#define Bad_CommOpen -101
#define Bad_CommClose -105

#define LPERR		-4  
#define CPERR		-3  
#define CMERR		-2  
#define ERR		-1
#define OK		0

#define ENQ  0x05
#define ACK  0x06
#define NAK  0x15
#define EOT  0x04
#define CAN  0x18
#define STX  0x02
#define ETX  0x03
#define US   0x1F

int APIENTRY GetSysVerion(HANDLE ComHandle, char *strVerion);
HANDLE APIENTRY CRT310NUOpen();
int APIENTRY CRT310NUClose(HANDLE ComHandle);
int APIENTRY GetDeviceCapabilities(HANDLE ComHandle, int *_InputReportByteLength, int *_OutputReportByteLength);
int APIENTRY ReadReport(HANDLE ComHandle,  BYTE _ReportData[],BYTE _ReportLen);
int APIENTRY WriteReport(HANDLE ComHandle,  BYTE _ReportData[],BYTE _ReportLen);
int APIENTRY USB_ExeCommand(HANDLE ComHandle,BYTE TxCmCode,BYTE TxPmCode,int TxDataLen,BYTE TxData[],BYTE *RxReplyType,BYTE *RxStCode1,BYTE *RxStCode0,int *RxDataLen,BYTE RxData[]);
int APIENTRY USB_ICCardTransmit(HANDLE ComHandle,BYTE TxAddr,BYTE TxCmCode,BYTE TxPmCode,int TxDataLen,BYTE TxData[],BYTE *RxReplyType,BYTE *RxCmCode,BYTE *RxPmCode,BYTE *RxStCode1,BYTE *RxStCode0,int *RxDataLen,BYTE RxData[]);

HANDLE APIENTRY CRT310NROpen(char *Port);
HANDLE APIENTRY CRT310NROpenWithBaut(char *Port, unsigned int Baudrate);
int APIENTRY CRT310NRClose(HANDLE ComHandle);
int  APIENTRY RS232_ExeCommand(HANDLE ComHandle,BYTE TxCmCode,BYTE TxPmCode,int TxDataLen,BYTE TxData[],BYTE *RxReplyType,BYTE *RxStCode1,BYTE *RxStCode0,int *RxDataLen,BYTE RxData[]);
int APIENTRY RS232_ICCardTransmit(HANDLE ComHandle,BYTE TxAddr,BYTE TxCmCode,BYTE TxPmCode,int TxDataLen,BYTE TxData[],BYTE *RxReplyType,BYTE *RxCmCode,BYTE *RxPmCode,BYTE *RxStCode1,BYTE *RxStCode0,int *RxDataLen,BYTE RxData[]);
int APIENTRY CancelCommand(HANDLE ComHandle);
