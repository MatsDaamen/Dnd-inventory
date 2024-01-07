import { getUserName } from "./auth";

let headers: HeadersInit;

function getBaseUrl(): string {
    return import.meta.env.VITE_API_URL + `/Inventory`
}

export function SetInventoryAuthHeaders(token: string) {
    headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    }
}

export type item = {
    id: number,
    name: string,
    discription: string,
    weight: number,
    price: number,
    type: string
}

export type inventory = {
    userId: string,
    ownerName: string,
    sessionId: number,
    items: item[]
}

export type AddItemRequest = {
    userId: string,
    sessionId: number,
    itemId: number,
    Amount: number
}

export type TransferItemRequest = {
    userId: string,
    sessionId: number,
    itemId: number,
    Amount: number
}

export const getInventory = async (sessionId: number, userId: string): Promise<inventory | null> => {
	
    let url = getBaseUrl() + `/${sessionId}/${userId}`
    
    const response = await fetch(url, {
        method: 'GET',
        headers: headers,
    });

	if (response.ok) {
		const inventory: inventory = await response.json();

        inventory.ownerName = await getUserName(inventory.userId);

		return inventory;
	}

    return null;
};

export const getAllInventoriesInSession = async (sessionId: number): Promise<inventory[] | null> => {
	
    let url = getBaseUrl() + `/${sessionId}`
    
    const response = await fetch(url, {
        method: 'GET',
        headers: headers,
    });

	if (response.ok) {
		const inventories: inventory[] = await response.json();

        for (let i = 0; i < inventories.length; i++) {
            inventories[i].ownerName = await getUserName(inventories[i].userId);
        }

		return inventories;
	}

    return null;
};