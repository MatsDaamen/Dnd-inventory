import { redirect } from "@sveltejs/kit";

function getUrl(url?: string): string {
    return `http://localhost:5254/api/Session${url}`
}

export type Session = {
    id: number,
    name: string,
    createdBy: number
};

export type joinKey = {
    sessionJoinKey: string,
    userId: number
};

export const getSessions = async (): Promise<Session[]> => {
	const response = await fetch(getUrl(), {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

	if (response.ok) {
		const sessions: Session[] = await response.json();
		return sessions;
	}

	return [];
};

export const getSession = async (id: number): Promise<Session> => {
	const response = await fetch(getUrl(id.toString()), {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        
    });

	if (response.ok) {
		const session: Session = await response.json();
		return session;
	}

	throw redirect(302, '/sessions');
};

export const createSession = async (session: Session) => {
    
        const response = await fetch(getUrl(), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(session)
        });
    
        return [];
    };

export const joinSession = async (joinRequestDto: joinKey) => {
    
	const response = await fetch(getUrl('Join'), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(joinRequestDto)
    });

	return [];
};