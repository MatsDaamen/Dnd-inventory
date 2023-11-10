import { redirect } from "@sveltejs/kit";

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
	const response = await fetch(`http://localhost:5254/api/Session`, {
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
	const response = await fetch(`http://localhost:5254/api/Session/${id}`, {
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
    
        const response = await fetch(`http://localhost:5254/api/Session`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(session)
        });
    
        return [];
    };

export const joinSession = async (joinRequestDto: joinKey) => {
    
	const response = await fetch(`http://localhost:5254/api/Session/Join`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(joinRequestDto)
    });

	return [];
};