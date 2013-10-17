using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpexGL.Framework.Media.Sound.MCI
{
    internal static class MCIAPI
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string cmd, StringBuilder ret, int retLen, IntPtr hwnd);

        /// <summary>
        /// Sends a command to the MCI-Device.
        /// </summary>
        /// <param name="cmd">The Command.</param>
        /// <param name="ret">The ReturnString.</param>
        /// <param name="retLen">The Length of the ReturnString.</param>
        /// <param name="hwnd">The Handle.</param>
        public static void SendCommand(string cmd, StringBuilder ret, int retLen, IntPtr hwnd)
        {
            var errorCode = mciSendString(cmd, ret, retLen, hwnd);

            //http://msdn.microsoft.com/en-us/library/aa228215(v=vs.60).aspx
            switch (errorCode)
            {
                case 0:
                    //success
                    break;
                case 5:
                    throw new InvalidOperationException("Invalid procedure call. (mciInvalidProcedureCall)");
                case 380:
                    //MSDN: "mciInvalidProertyValue" we do not realy figure out if it is just a spelling error
                    throw new InvalidOperationException("Invalid property value. (mciInvalidProertyValue)");
                case 383:
                    throw new InvalidOperationException("Property is read-only. (mciSetNotSupported)");
                case 394:
                    throw new InvalidOperationException("Property is write-only. (mciGetNotSupported)");
                case 425:
                    throw new InvalidOperationException("Invalid object use. (mciInvalidObjectUse)");
                case 461:
                    throw new InvalidOperationException(
                        "Specified format doesn't match format of data. (mciWrongClipboardFormat)");
                case 672:
                    throw new InvalidOperationException(
                        "DataObject formats list may not be cleared or expanded outside of the OLEStartDrag event. (mciObjectLocked)");
                case 673:
                    throw new InvalidOperationException("Expected at least one argument. (mciExpectedArgument)");
                case 674:
                    throw new InvalidOperationException(
                        "Illegal recursive invocation of OLE drag and drop. (mciRecurvsiveOleDrag)");
                case 675:
                    throw new InvalidOperationException(
                        "Non-intrinsic OLE drag and drop formats used with SetData require Byte array data. GetData may return more bytes than were given to SetData. (mciFormatNotByteArray)");
                case 676:
                    throw new InvalidOperationException(
                        "Requested data was not supplied to the DataObject during the OLESetData event. (mciDataNotSetForFormat)");
                case 30001:
                    throw new InvalidOperationException("Can't create button. (mciCantCreateButton)");
                case 30002:
                    throw new InvalidOperationException("Can't create a timer resource. (mciCantCreateTimer)");
                case 30004:
                    throw new InvalidOperationException("Unsupported function. (mciUnsupportedFunction)");
                //MCI Error Strings:
                case 256:
                    throw new InvalidOperationException("MCIERR_BASE");
                case 257:
                    throw new InvalidOperationException("MCIERR_INVALID_DEVICE_ID");
                case 259:
                    throw new InvalidOperationException("MCIERR_UNRECOGNIZED_KEYWORD");
                case 261:
                    throw new InvalidOperationException("MCIERR_UNRECOGNIZED_COMMAND");
                case 262:
                    throw new InvalidOperationException("MCIERR_HARDWARE");
                case 263:
                    throw new InvalidOperationException("MCIERR_INVALID_DEVICE_NAME");
                case 264:
                    throw new InvalidOperationException("MCIERR_OUT_OF_MEMORY");
                case 265:
                    throw new InvalidOperationException("MCIERR_DEVICE_OPEN");
                case 266:
                    throw new InvalidOperationException("MCIERR_CANNOT_LOAD_DRIVER");
                case 267:
                    throw new InvalidOperationException("MCIERR_MISSING_COMMAND_STRING");
                case 268:
                    throw new InvalidOperationException("MCIERR_PARAM_OVERFLOW");
                case 269:
                    throw new InvalidOperationException("MCIERR_MISSING_STRING_ARGUMENT");
                case 270:
                    throw new InvalidOperationException("MCIERR_BAD_INTEGER");
                case 271:
                    throw new InvalidOperationException("MCIERR_PARSEER_INTERNAL");
                case 272:
                    throw new InvalidOperationException("MCIERR_DRIVER_INTERNAL");
                case 273:
                    throw new InvalidOperationException("MCIERR_MISSING_PARAMETER");
                case 274:
                    throw new InvalidOperationException("MCIERR_UNSUPPORTED_FUNCTION");
                case 275:
                    throw new InvalidOperationException("MCIERR_FILE_NOT_FOUND");
                case 276:
                    throw new InvalidOperationException("MCIERR_DEVICE_NOT_READY");
                case 277:
                    throw new InvalidOperationException("MCIERR_INTERNAL");
                case 278:
                    throw new InvalidOperationException("MCIERR_DRIVER");
                case 279:
                    throw new InvalidOperationException("MCIERR_CANNOT_USE_ALL");
                case 280:
                    throw new InvalidOperationException("MCIERR_MULTIPLE");
                case 281:
                    throw new InvalidOperationException("MCIERR_EXTENSION_NOT_FOUND");
                case 282:
                    throw new InvalidOperationException("MCIERR_OUTOFRANGE");
                case 283:
                    throw new InvalidOperationException("MCIERR_FLAGS_NOT_COMPATIBLE");
                case 286:
                    throw new InvalidOperationException("MCIERR_FILE_NOT_SAVED");
                case 287:
                    throw new InvalidOperationException("MCIERR_DEVICE_TYPE_REQUIRED");
                case 288:
                    throw new InvalidOperationException("MCIERR_DEVICE_LOCKED");
                case 289:
                    throw new InvalidOperationException("MCIERR_DUPLICATE_ALIAS");
                case 290:
                    throw new InvalidOperationException("MCIERR_BAD_CONSTANT");
                case 291:
                    throw new InvalidOperationException("MCIERR_MUST_USE_SHAREABLE");
                case 292:
                    throw new InvalidOperationException("MCIERR_MISSING_DEVICE_NAME");
                case 293:
                    throw new InvalidOperationException("MCIERR_BAD_TIME_FORMAT");
                case 294:
                    throw new InvalidOperationException("MCIERR_NO_CLOSING_QUOTE");
                case 295:
                    throw new InvalidOperationException("MCIERR_DUPLICATE_FLAGS");
                case 296:
                    throw new InvalidOperationException("MCIERR_INVALID_FILE");
                case 297:
                    throw new InvalidOperationException("MCIERR_NULL_PARAMETER_BLOCK");
                case 298:
                    throw new InvalidOperationException("MCIERR_UNNAMED_RESOURCE");
                case 299:
                    throw new InvalidOperationException("MCIERR_NEW_REQUIRES_ALIAS");
                case 300:
                    throw new InvalidOperationException("MCIERR_NOTIFY_ON_AUTO_OPEN");
                case 301:
                    throw new InvalidOperationException("MCIERR_NO_ELEMENT_ALLOWED");
                case 302:
                    throw new InvalidOperationException("MCIERR_NONAPPLICABLE_FUNCTION");
                case 303:
                    throw new InvalidOperationException("MCIERR_ILLEGAL_FOR_AUTO_OPEN");
                case 304:
                    throw new InvalidOperationException("MCIERR_FILENAME_REQUIRED");
                case 305:
                    throw new InvalidOperationException("MCIERR_EXTRA_CHARACTERS");
                case 306:
                    throw new InvalidOperationException("MCIERR_DEVICE_NOT_INSTALLED");
                case 307:
                    throw new InvalidOperationException("MCIERR_GET_CD");
                case 308:
                    throw new InvalidOperationException("MCIERR_SET_CD");
                case 309:
                    throw new InvalidOperationException("MCIERR_SET_DRIVE");
                case 310:
                    throw new InvalidOperationException("MCIERR_DEVICE_LENGTH");
                case 311:
                    throw new InvalidOperationException("MCIERR_DEVICE_ORD_LENGTH");
                case 312:
                    throw new InvalidOperationException("MCIERR_NO_INTEGER");
                case 320:
                    throw new InvalidOperationException("MCIERR_WAVE_OUTPUTSINUSE");
                case 321:
                    throw new InvalidOperationException("MCIERR_WAVE_SETOUTPUTINUSE");
                case 322:
                    throw new InvalidOperationException("MCIERR_WAVE_INPUTSINUSE");
                case 323:
                    throw new InvalidOperationException("MCIERR_WAVE_SETINPUTINUSE");
                case 324:
                    throw new InvalidOperationException("MCIERR_WAVE_OUTPUTUNSPECIFIED");
                case 325:
                    throw new InvalidOperationException("MCIERR_WAVE_INPUTUNSPECIFIED");
                case 326:
                    throw new InvalidOperationException("MCIERR_WAVE_OUTPUTSUNSUITABLE");
                case 327:
                    throw new InvalidOperationException("MCIERR_WAVE_SETOUTPUTUNSUITABLE");
                case 328:
                    throw new InvalidOperationException("MCIERR_WAVE_INPUTSUNSUITABLE");
                case 329:
                    throw new InvalidOperationException("MCIERR_WAVE_SETINPUTUNSUITABLE");
                case 336:
                    throw new InvalidOperationException("MCIERR_SEQ_DIV_INCOMPATIBLE");
                case 337:
                    throw new InvalidOperationException("MCIERR_SEQ_PORT_INUSE");
                case 338:
                    throw new InvalidOperationException("MCIERR_SEQ_PORT_NONEXISTENT");
                case 339:
                    throw new InvalidOperationException("MCIERR_SEQ_PORT_MAPNODEVICE");
                case 340:
                    throw new InvalidOperationException("MCIERR_SEQ_PORT_MISCERROR");
                case 341:
                    throw new InvalidOperationException("MCIERR_SEQ_TIMER");
                case 342:
                    throw new InvalidOperationException("MCIERR_SEQ_PORTUNSPECIFIED");
                case 343:
                    throw new InvalidOperationException("MCIERR_SEQ_NOMIDIPRESENT");
                case 346:
                    throw new InvalidOperationException("MCIERR_NO_WINDOW");
                case 347:
                    throw new InvalidOperationException("MCIERR_CREATEWINDOW");
                case 348:
                    throw new InvalidOperationException("MCIERR_FILE_READ");
                case 349:
                    throw new InvalidOperationException("MCIERR_FILE_WRITE");
                case 512:
                    throw new InvalidOperationException("MCIERR_CUSTOM_DRIVER_BASE");
            }
        }
    }
}
