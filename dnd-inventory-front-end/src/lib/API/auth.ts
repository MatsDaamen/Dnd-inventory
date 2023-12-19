import { SetAuthHeaders } from "./sessions";

let headers = {
    'Content-Type': 'application/json',
    'authorization': 'Bearer ' + import.meta.env.VITE_AUTH_API_TOKEN
}

function getBaseUrl(): string {
        return import.meta.env.VITE_AUTH0_DOMAIN
}

export const getUserId = async (email: string): Promise<string | null> => {

    let url = getBaseUrl() + '/api/v2/users-by-email?'  + new URLSearchParams({email: email});

	const response = await fetch(url, 
    {
        method: 'GET',
        headers: headers,

    });

	if (response.ok) {
        const responseBody: [{ user_id: string}] = await response.json();
        let user = responseBody.pop();

        let id = user?.user_id;

        SetAuthHeaders('');

        return id;
	}

	return null;
};

export const getAccessToken = async (): Promise<string | null> => {

    let url = getBaseUrl() + '/oauth/token';

	const response = await fetch(url, 
    {
        method: 'POST',
        headers: headers,
        body: JSON.stringify({
            grant_type: 'client_credentials',
            client_id: import.meta.env.VITE_AUTH0_ID,
            client_secret: import.meta.env.VITE_AUTH0_SECRET,
            audience: import.meta.env.VITE_AUTH0_API_AUDIENCE
          })
    });

	if (response.ok) {
        const responseBody: {access_token: string} = await response.json();
        return responseBody.access_token;
	}

	return null;
};