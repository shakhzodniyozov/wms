import { axios } from "../axios/axios";

class EngineService {
    #baseUrl = "engine";

    async create(data) {
        return await axios.post(this.#baseUrl, data);
    }

    async update(data) {
        return await axios.put(this.#baseUrl, data);
    }

    async getById(id) {
        return await axios.get(`${this.#baseUrl}/${id}`);
    }

    async getAll() {
        return await axios.get(this.#baseUrl);
    }

    async delete(id) {
        return await axios.delete(`${this.#baseUrl}/${id}`);
    }
}

export default new EngineService();