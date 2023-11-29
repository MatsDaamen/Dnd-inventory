import { MongoClient } from 'mongodb';

function getUri() {
	return 'mongodb://root:mongopwd@localhost:8080';
}

const clientPromise: Promise<MongoClient> = new MongoClient(getUri()).connect();

export default clientPromise;