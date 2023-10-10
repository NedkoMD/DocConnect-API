namespace DocConnect.Business.Extensions
{
    public static class EmailSenderExtensions
    {

        public const string EmailVerificationTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""UTF-8"">
            <title>DocConnect Email Verification</title>
        </head>
        <body style=""background-color: #F0FFF4; font-family: Arial, sans-serif; margin: 0; padding: 20px; background-image: url('https://docconnect-green.test.devsmm.com/emailWave.png'), url('https://docconnect-green.test.devsmm.com/emailWave.png'); background-repeat: no-repeat, no-repeat; background-position: left bottom, right bottom;"">
            <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" margin=""0 auto"">
                <tr>
                    <td align=""center"" valign=""center"">
                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""20"" bgcolor=""#ffffff"" style=""border-radius: 10px; box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);"">
                            <tr>
                                <td align=""center"">
                                    <img src=""https://docconnect-green.test.devsmm.com/emailLogo.png"" alt=""SVG Image"" />
                                    <h2 style=""font-size: 24px; font-weight: 400; margin-top: 20px;"">Email Verification</h2>
                                    <p style=""font-size: 16px; font-weight: 400;"">To finish setting up your DocConnect account, please verify your email address.</p>
                                    <p style=""font-size: 16px; font-weight: 500;""><a href=""{0}"" style=""text-decoration: none; padding: 10px; background-color: #38A169; height: 30px; width: 30%; border-radius: 5px; border: none;  color: #ffff;"">Verify my Email address</a></p>
                                    <p style=""font-size: 13px; font-weight: 300;"">Best regards, DocConnect!</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";

        public const string PasswordResetTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""UTF-8"">
            <title>DocConnect Email Verification</title>
        </head>
        <body style=""background-color: #F0FFF4; font-family: Arial, sans-serif; margin: 0; padding: 20px; background-image: url('https://docconnect-green.test.devsmm.com/emailWave.png'), url('https://docconnect-green.test.devsmm.com/emailWave.png'); background-repeat: no-repeat, no-repeat; background-position: left bottom, right bottom;"">
            <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                    <td align=""center"" valign=""center"">
                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""20"" bgcolor=""#ffffff"" style=""border-radius: 10px; box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);"">
                            <tr>
                                <td align=""center"">
                                    <img src=""https://docconnect-green.test.devsmm.com/emailLogo.png"" alt=""SVG Image"" />
                                    <h2 style=""font-size: 24px; font-weight: 400; margin-top: 20px;"">Reset Password</h2>
                                    <div style=""font-size: 16px; font-weight: 400; text-align: justify;"">
                                        <p style=""font-size: 16px; font-weight: 400;"">Dear {0} {1}, you recently requested to reset your password. To reset your password, please click on the button below.</p>
                                    </div>
                                    <p style=""font-size: 16px; font-weight: 500;""><a href=""{2}"" style=""text-decoration: none; padding: 10px 20px; background-color: #38A169; height: 30px; width: 30%; border-radius: 5px; border: none;  color: #ffff;"">Reset Password</a></p>
                                    <p style=""font-size: 13px; font-weight: 300;"">If you did not request to reset your password, please ignore this email.</p>
                                    <p style=""font-size: 13px; font-weight: 300;"">Best regards, DocConnect!</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";

        public const string AppointmentCancellationTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""UTF-8"">
            <title>DocConnect Appointment Cancellation</title>
        </head>
        <body style=""background-color: #F0FFF4; font-family: Arial, sans-serif; margin: 0; padding: 20px; background-image: url('https://docconnect-green.test.devsmm.com/emailWave.png'), url('https://docconnect-green.test.devsmm.com/emailWave.png'); background-repeat: no-repeat, no-repeat; background-position: left bottom, right bottom;"">
            <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                <tr>
                    <td align=""center"" valign=""center"">
                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""20"" bgcolor=""#ffffff"" style=""border-radius: 10px; box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);"">
                            <tr>
                                <td align=""center"">
                                    <img src=""https://docconnect-green.test.devsmm.com/emailLogo.png"" alt=""SVG Image"" />
                                    <h2 style=""font-size: 24px; font-weight: 400; margin-top: 20px;"">Appointment Cancellation Notification</h2>
                                    <div style=""font-size: 16px; font-weight: 400; text-align: justify;"">
                                        <p style=""font-size: 16px; font-weight: 400;"">Dear {0}, your appointment with doctor {1} ({2}) for {3} was cancelled.</p>
                                    </div>
                                    <p style=""font-size: 13px; font-weight: 300;"">Best regards, DocConnect!</p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>";
    }
}
