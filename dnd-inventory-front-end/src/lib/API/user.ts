import { redirect } from "@sveltejs/kit";

function getBaseUrl(): string {
        return `http://localhost:5254/api/User`
}

export type User = {
    id: number,
    name: string
};

export const getuser = async (): Promise<User | null> => {
	const response = await fetch(getBaseUrl(), {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

	if (response.ok) {
		const user: User = {
            id: 10,
            name: "test",
        }

        console.log(user);

		return user;
	}

    return null;
};