import { SvelteKitAuth, type SvelteKitAuthConfig } from '@auth/sveltekit';
import type { Handle } from '@sveltejs/kit';
import type { Provider } from '@auth/core/providers';

const config: SvelteKitAuthConfig = {
    providers: [{
      id: 'auth0',
      name: 'Auth0',
      type: 'oidc',
      clientId: import.meta.env.VITE_AUTH0_ID,
      clientSecret: import.meta.env.VITE_AUTH0_SECRET,
      issuer: import.meta.env.VITE_AUTH0_DOMAIN
    } as Provider],
    secret: 'cfc1bb18fc9ba615ea8a3f6db2df089c',
    trustHost: true,
    debug: false,
    session: {
      maxAge: 1800 // 30 mins
    },
    callbacks: {
        async jwt({ token, account }) {
            if (account) {
                token.access_token = account.access_token;
                token.id_token = account.id_token;
            }
            return token;
        }
    }
  };
  
  export const handle = SvelteKitAuth(config) satisfies Handle;