import { Auth0Client, createAuth0Client, type Auth0ClientOptions } from "@auth0/auth0-spa-js";
import { getUser, user } from "./authStore";
import { SetAuthHeaders } from "$lib/API/sessions";

async function createClient() {
    let auth0Client = await createAuth0Client({
      domain: import.meta.env.VITE_AUTH0_DOMAIN,
      clientId: import.meta.env.VITE_AUTH0_ID,
      authorizationParams: {
        redirect_uri: "http://localhost:4173/login",
      },
      useRefreshTokens: true
    });
  
    return auth0Client;
  }
  
  async function login(client: Auth0Client, options?:  Auth0ClientOptions) {
    await client.loginWithPopup(options).then(async () => {
      user.set(await client.getUser());
      SetAuthHeaders(await client.getTokenSilently());
	    console.log(getUser());
    });
  }
  
  function logout(client: Auth0Client) {
    return client.logout();
  }
  
  const auth = {
    createClient,
    login,
    logout
  };
  
  export default auth;