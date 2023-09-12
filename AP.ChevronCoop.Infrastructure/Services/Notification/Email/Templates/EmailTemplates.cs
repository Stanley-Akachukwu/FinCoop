namespace AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;

public class EmailTemplates
{

    #region Template

    private const string Logo = "https://chevroncoop-uat-web.azurewebsites.net/images/chevron/landingpage/logo.svg";

    private const string BaseTemplate = @"
    <!DOCTYPE html>
    <html lang='en'>
      <head>
        <meta charset='utf-8' />
        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
        <title>Email template</title>
        <style>
          @import url('https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700;800&display=swap');

          body {
            background-color: #ffffff;
            padding: 8px 6px 0 6px;
            font-family: 'Inter', sans-serif;
          }

          .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
          }

          .top {
            /* height: 50%; */
            padding: 20px 40px 24px 40px;
            background-color: #1f82bd;
            position: relative;
          }

          .logo {
            width: 60px;
            height: 60px;
          }

          .top-wrapper {
            width: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            word-wrap: break-word;
            text-align: center;
          }

          h2 {
            font-size: 30px;
            line-height: 36px;
            font-weight: 600;
            color: #ffffff;
            margin: 0;
            padding: 0.5rem 0;
            text-align: center;
          }

          .top-text {
            color: #ffffff;
            font-size: 14px;
            font-weight: 500;
            margin-bottom: 40px;
            word-wrap: break-word;
            text-align: center;
          }

          .bottom {
            height: 50%;
            padding: 20px 40px 24px 40px;
            background-color: #ffffff;
            position: relative;
          }

          .bottom-wrapper {
            width: 100%;
            display: flex;
            flex-wrap: wrap;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            word-wrap: break-word;
            text-align: center;
          }

          .OTP {
            font-weight: 700;
            font-size: 48px;
            line-height: 150%;
            margin: 0;
          }

          .otpText {
            font-weight: 500;
            font-size: 18px;
            line-height: 150%;
            color: #1f1f1f;
            padding-bottom: 20px;
          }

          .bottom-text{
            font-family: 'Inter', sans-serif;
            font-style: normal;
            font-weight: 500;
            font-size: 14px;
            line-height: 160%;
            text-align: center;
            color: rgba(31, 31, 31, 0.5);
            margin-bottom: 20px;
          }

          .bottom-link{
            font-family: 'Inter', sans-serif;
            font-style: normal;
            font-weight: 600;
            font-size: 14px;
            line-height: 160%;
            text-align: center;
            text-decoration-line: underline;
            color: #683AB7;
            padding: 20px 20px;
          }

          .logo-two
            {
                position: absolute;
                top: 75%;
                right: 0;
                z-index: 10;
            }
            
            button {
              width: 200px;
              height: 50px;
              background-color: #1f82bd;
              color: #ffffff;
              border: none;
              border-radius: 5px;
              font-size: 16px;
              font-weight: bold;
              cursor: pointer;
            }
        </style>
     

      </head>
      <body>
        {{Body}}
      </body>
    </html>
  ";

    #endregion

    public static string CreateEnrollmentEmail(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    Welcome to Chevron Employees Multipurpose Cooperative Society (CEMCS)! We're delighted to have you as part of 
                    our valued community.
                    <br /> <br />
                    Our mission is to provide you with exceptional service and unforgettable experiences. 
                    Whether you're here for our loan offerings, compulsory savings, insurance offerings, travel and tour packages, 
                    we're dedicated to meeting your needs and exceeding your expectations. Feel free to browse our wide range of services 
                    and discover the perfect fit for your requirements. Should you have any questions or require assistance, 
                    our friendly team is here to help, just send a message to us at itsupport@Africaprudential.com.
                    <br /> <br />
                    Thank you for being a part of our community. We look forward to serving you 
                    and making your time with us truly exceptional!
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Please click the button below to verify your email.
                    </p>
                    
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>Verify Email</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateGuarantorApproval(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    You have a new Guarantor request!
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Click the button below to view pending approvals.
                    </p>
                    
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>View Approvals</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateApprovalNotification(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    You have a new approval request!
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Click the button below to view pending approvals.
                    </p>
                    
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>View Approvals</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");

        return template;
    }

    public static string CreateApprovalRejectionNotification(string name, string link, string description)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    <br />
                    {description} <br /> <br />
                  </p>
                </div>
              </div>
            </div>
          </div>
        ");

        return template;
    }

    public static string CreateDepositApplicationApprovalAlert(string name, string depositName)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                      Congratulation! Your {depositName} deposit application has been approved and an account has been created.
                      <br/>Kindly login to your dashboard to review
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

         </div>
        ");


        return template;
    }


    public static string CreateDepositActionApprovalAlert(string name, string actionMessage)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                      Congratulation! Your {actionMessage} has been approved.<br/> Kindly login to your dashboard to review
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

         </div>
        ");


        return template;
    }


    public static string CreateDepositWorkflowApprovalAlert(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    A request has been sent for your approval.
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
                    <p>Kindly follow the link bellow to process your approval. </p>
                    <p>Approval link: <a href='{link}'> {link} </a></p>

                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>

                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateLoanApproval(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    Congratulation! Your loan account has been approved.
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Click the button below to view your loans.
                    </p>
                    
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>View Approvals</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateLoanProductAlert(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    A request has been sent for your approval.
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
                    <p>Kindly follow the link bellow to process your approval. </p>
                    <p>Approval link: <a href='{link}'> {link} </a></p>

                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>

                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateApplicationUserEmail(string name, string link, string password)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    <b>Welcome to CEMCS!</b>
                    <br /> <br /> <br />
                    An account has been created for you on CEMCS. Kindly follow the link bellow to access your account and update your password.
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Your account details
                    </p>
                    <div style='text-align:left;'>
                    	<p><b>Email</b>: <i>your email address</i></p>
                        <p><b>Password</b>: {password}</p>
                    </div>
                    <br /> <br />
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>Visit website</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateForgotPasswordEmail(string otp, string name)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    You recently requested to reset your password.
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
                    <h1 class='OTP'>{otp}</h1>
            
                    <p class='otpText'>
                      Use this OTP to reset your password
                    </p>
                    <p class='bottom-text' style='color:red'>
                      kindly disregard the mail if you did not initiate the password reset request
                    </p>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateApprovalEmail(string name, string link)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                    Thanks for signing up with CEMCS! Before you get started accepting
                    payments with CEMCS, we need you to confirm your email address.
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
            
                    <p class='otpText'>
                      Please click the button below to verify your email.
                    </p>
                    
                    <div style='text-align: center; margin-bottom: 30px;'>
                    	<a href='{link}' class='bottom-link' >
                        	<button>Verify Email</button>
                       	</a>
                    </div>
                    
                    
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreatePendingPayrollPayment(string name, string link)
    {

        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @$"
          <div class='container'>
            <div href='#' class='top'>
              <img src='https://example.com/logo.png' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, {name}</h2>
                  <p class='top-text'>
                   Pending Payroll Payment
                    <br /> <br /> <br />
                  </p>
                </div>
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
                    <p>Kindly follow the link bellow to process your approval. </p>
                    <p>Approval link: <a href='{link}'> {link} </a></p>

                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>

                    <a href='{link}' class='bottom-link' >
                      {link}
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }

    public static string CreateBaseEmail(dynamic model)
    {
        var template = BaseTemplate;
        template = template.Replace("{{Body}}", @"
          <div class='container'>
            <div href='#' class='top'>
              <img src='{Logo}' alt='Logo' class='logo' />
              <div class='top-wrapper'>
                <div>
                  <h2>Hello, Olusola</h2>
                  <p class='top-text'>
                    Thanks for signing up with CEMCS! Before you get started accepting
                    payments with CEMCS, we need you to confirm your email address.
                    <br /> <br /> <br />
                    Please click the button below to complete your signup.
                  </p>
                </div>
              </div>

              <div class='logo-two'>
                <img
                  src='{Logo}'
                  alt='Logo 2'
                />
              </div>
            </div>

            <div class='bottom'>
              <div class='bottom-wrapper'>
                <div>
                    <h1 class='OTP'>635 270</h1>
            
                    <p class='otpText'>
                      Use this OTP to verify your account
                    </p>
                    <p class='bottom-text'>
                      If you have any trouble clicking the button above, please copy and
                      paste the URL below into your web browser.
                    </p>
                    <a
                      href='#'
                      class='bottom-link'
                    >
                      https://dashboard.CEMCS.com/#/confirm-email/1620f51b796
                      891597230304085b19cc6b5a2d6e3
                    </a>
                  </div>
                </div>
            </div>
          </div>
        ");


        return template;
    }
}
