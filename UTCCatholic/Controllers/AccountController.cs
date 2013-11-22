using UTCCatholic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFF.Controllers
{
	/// <summary>
	/// This Controller handles all Account Actions, such as Logging In, Registering, Changing Credentials, etc.
	/// </summary>
    [Authorize]
	[RequireHttps]
    public class AccountController : Controller
    {
		protected UserManager<AppUser> UserManager { get; private set; }
		private const string XsrfKey = "XsrfId";

		protected AppDbContext db = new AppDbContext();
		//protected AuthenticationIdentityManager IdentityManager;
		protected Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

		public AccountController()
        {
			this.UserManager = new UserManager<AppUser>( new UserStore<AppUser>( this.db ) );
        }
		public AccountController( UserManager<AppUser> userManager )
        {
            this.UserManager = userManager;
        }

		#region Logging System
			protected override void HandleUnknownAction( string actionName )
			{
				this.Login();
			}
			[AllowAnonymous]
			public ActionResult Login( String returnUrl = null )
			{
				ViewBag.ReturnUrl = returnUrl;
				return View("Login");
			}
			//
			// POST: /Account/Disassociate
			[HttpPost]
			[ValidateAntiForgeryToken]
			public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
			{
				ManageMessageId? message = null;

				var result = await UserManager.RemoveLoginAsync( User.Identity.GetUserId(), new UserLoginInfo( loginProvider, providerKey ) );
				if ( result.Succeeded )
				{
					message = ManageMessageId.RemoveLoginSuccess;
				}
				else
				{
					message = ManageMessageId.Error;
				}

				return RedirectToAction("Manage", new { Message = message });
			}

			//
			// GET: /Account/Manage
			public PartialViewResult Manage( ManageMessageId? message )
			{
				ViewBag.StatusMessage =
					message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
					: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
					: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
					: message == ManageMessageId.Error ? "An error has occurred."
					: "";
				ViewBag.HasLocalPassword =  HasPassword();
				ViewBag.ReturnUrl = Url.Action("Manage");
				return PartialView();
			}
			/*
			//
			// POST: /Account/Manage
			[HttpPost]
			[ValidateAntiForgeryToken]
			public ActionResult Manage(ManageUserViewModel model)
			{
				string userId = Account.User.Id;
				bool hasLocalLogin = IdentityManager.Logins.HasLocalLogin(userId);
				ViewBag.HasLocalPassword = hasLocalLogin;
				ViewBag.ReturnUrl = Url.Action("Manage");
				if (hasLocalLogin)
				{               
					if (ModelState.IsValid)
					{
						var result = IdentityManager.Passwords.ChangePassword(User.Identity.GetUserName(), model.OldPassword, model.NewPassword);
						if (result.Success)
						{
							return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
						}
						else
						{
							ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
						}
					}
				}
				else
				{
					// User does not have a local password so remove any validation errors caused by a missing OldPassword field
					ModelState state = ModelState["OldPassword"];
					if (state != null)
					{
						state.Errors.Clear();
					}

					if (ModelState.IsValid)
					{
						try
						{
							// Create the local login info and link the local account to the user
							var result = IdentityManager.Logins.AddLocalLogin( userId, User.Identity.GetUserName(), model.NewPassword );
							if ( result.Success )
							{
								return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
							}
							else
							{
								ModelState.AddModelError("", "Failed to set password");
							}
						}
						catch (Exception e)
						{
							ModelState.AddModelError("", e);
						}
					}
				}

				// If we got this far, something failed, redisplay form
				return PartialView(model);
			}
			*/
			//
			// POST: /Account/ExternalLogin
			[HttpPost]
			[AllowAnonymous]
			[ValidateAntiForgeryToken]
			public ActionResult ExternalLogin(string provider, string returnUrl)
			{
				// Request a redirect to the external login provider
				return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "User", new { ReturnUrl = returnUrl }));
			}
			[HttpPost]
			[ValidateAntiForgeryToken]
			public ActionResult LinkLogin( string provider )
			{
				// Request a redirect to the external login provider to link a login for the current user
				return new ChallengeResult( provider, Url.Action( "LinkLoginCallback", "User" ), User.Identity.GetUserId() );
			}
			//
			// GET: /Account/ExternalLoginCallback
			[AllowAnonymous]
			public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
			{
				var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
				if ( loginInfo == null )
				{
					return RedirectToAction( "Login" );
				}
				// Sign in the user with this external login provider if the user already has a login
				var user = await UserManager.FindAsync( loginInfo.Login );
				if ( user != null )
				{
					await SignInAsync( user, isPersistent: false );
					return RedirectToLocal( returnUrl );
				}
				else
				{
					// If the user does not have an account, then prompt the user to create an account
					ViewBag.ReturnUrl = returnUrl;
					ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
					return View( "ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName } );
				}
			}
			public async Task<ActionResult> LinkLoginCallback()
			{
				var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync( XsrfKey, User.Identity.GetUserId() );
				if ( loginInfo == null )
				{
					return RedirectToAction( "Manage", new { Message = ManageMessageId.Error } );
				}
				var result = await UserManager.AddLoginAsync( User.Identity.GetUserId(), loginInfo.Login );
				if ( result.Succeeded )
				{
					return RedirectToAction( "Manage" );
				}
				return RedirectToAction( "Manage", new { Message = ManageMessageId.Error } );
			}
			//
			// POST: /Account/ExternalLoginConfirmation
			[HttpPost]
			[AllowAnonymous]
			[ValidateAntiForgeryToken]
			public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
			{
				if (User.Identity.IsAuthenticated)
				{
					return RedirectToAction("Manage");
				}
            
				if (ModelState.IsValid)
				{
					// Get the information about the user from the external login provider
					var info = await AuthenticationManager.GetExternalLoginInfoAsync();
					if ( info == null )
					{
						return View( "ExternalLoginFailure" );
					}
					var user = new AppUser( model.UserName );
					var result = await UserManager.CreateAsync( user );
					if ( result.Succeeded )
					{
						result = await UserManager.AddLoginAsync( user.Id, info.Login );
						if ( result.Succeeded )
						{
							await SignInAsync( user, isPersistent: false );
							return RedirectToLocal( returnUrl );
						}
					}
					else
					{
						AddErrors( result );
					}
				}
				ViewBag.ReturnUrl = returnUrl;
				return View( model );
			}

			//
			// POST: /Account/LogOff
			[HttpPost]
			[ValidateAntiForgeryToken]
			public ActionResult LogOff()
			{
				FormsAuthentication.SignOut();
				AuthenticationManager.SignOut();
				return RedirectToAction("Index", "Store");
			}

			//
			// GET: /Account/ExternalLoginFailure
			[AllowAnonymous]
			public ActionResult ExternalLoginFailure()
			{
				return View();
			}

			[ChildActionOnly]
			public ActionResult RemoveAccountList()
			{
				var linkedAccounts = UserManager.GetLogins( User.Identity.GetUserId() );
				ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
				return (ActionResult) PartialView( "_RemoveAccountPartial", linkedAccounts );
			}

			protected override void Dispose( bool disposing )
			{
				if ( disposing && UserManager != null )
				{
					db.Dispose();
					UserManager.Dispose();
					UserManager = null;
				}
				base.Dispose( disposing );
			}
		#endregion
		#region Helpers
			private ActionResult RedirectToLocal(string returnUrl)
			{
				if (Url.IsLocalUrl(returnUrl))
				{
					return Redirect(returnUrl);
				}
				else
				{
					return RedirectToAction("Index", "Store");
				}
			}

			private async Task SignInAsync( AppUser user, bool isPersistent )
			{
				AuthenticationManager.SignOut( DefaultAuthenticationTypes.ExternalCookie );
				var identity = await UserManager.CreateIdentityAsync( user, DefaultAuthenticationTypes.ApplicationCookie );
				AuthenticationManager.SignIn( new AuthenticationProperties() { IsPersistent = isPersistent }, identity );
			}

			private class ChallengeResult : HttpUnauthorizedResult
			{
				public ChallengeResult( string provider, string redirectUri )
					: this( provider, redirectUri, null )
				{
				}
				public ChallengeResult( string provider, string redirectUri, string userId )
				{
					LoginProvider = provider;
					RedirectUri = redirectUri;
					UserId = userId;
				}
				public string LoginProvider { get; set; }
				public string RedirectUri { get; set; }
				public string UserId { get; set; }
				public override void ExecuteResult( ControllerContext context )
				{
					var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
					if ( UserId != null )
					{
						properties.Dictionary[XsrfKey] = UserId;
					}
					context.HttpContext.GetOwinContext().Authentication.Challenge( properties, LoginProvider );
				}
			}
			private void AddErrors( IdentityResult result )
			{
				foreach ( var error in result.Errors )
				{
					ModelState.AddModelError( "", error );
				}
			}
			private bool HasPassword()
			{
				var user = UserManager.FindById( User.Identity.GetUserId() );
				if ( user != null )
				{
					return user.PasswordHash != null;
				}
				return false;
			}
			public enum ManageMessageId
			{
				ChangePasswordSuccess,
				SetPasswordSuccess,
				RemoveLoginSuccess,
				Error
			}
		#endregion
    }
}