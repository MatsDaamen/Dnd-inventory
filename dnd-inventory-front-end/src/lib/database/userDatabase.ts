import clientPromise from "./clientPromise";
import { ObjectId, type Collection, type Db, type MongoClient, type WithId } from 'mongodb';



interface DatabaseUserAccount {
    _id?: ObjectId,
    name: string,
    email: string,
    image: string
}

interface DatabaseAccount {
	_id: ObjectId,
	access_token: string,
	id_token: string,
	expires_at: number,
	scope: string,
	token_type: string,
	providerAccountId: string,
	provider: string,
	type: string,
	userId: ObjectId
}

export interface UserAccount {
    _id?: ObjectId,
    name: string,
    email: string,
    image: string
}

export class UserDatabase {
    private db: Db;
    private userCollection: Collection<DatabaseUserAccount>;
    private accountCollection: Collection<DatabaseAccount>;

	constructor(db: Db) {
		this.db = db;
		this.userCollection = this.db.collection('users');
		this.accountCollection = this.db.collection('accounts');
	}

	static async fromClient(client: Promise<MongoClient>): Promise<UserDatabase> {
		return new UserDatabase((await client).db("accounts"));
	}

    async getUserByEmail(email: string): Promise<UserAccount | null> {
		const user = await this.userCollection.findOne({ email: email });

        if (!user) return null;

		return {
			_id: user._id,
			name: user.name,
			email: user.email,
			image: user.image
		};
	}

    async getUserIdByEmail(email: string): Promise<string | null> {
		const user = await this.userCollection.findOne({ email: email });

        if (!user) return null;

		return user._id.toString();
	}
	
	async getUserNameById(id: string): Promise<string | null> {
		const user = await this.userCollection.findOne({ _id: new ObjectId(id) });

        if (!user) return null;

		return user.name;
	}

	async getAccountTokenByUserId(id: ObjectId): Promise<string | null> {
		const account = await this.accountCollection.findOne({ userId: new ObjectId(id) });

        if (!account) return null;

		return account.id_token;
	}
}