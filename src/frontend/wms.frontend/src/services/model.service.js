import { axios } from "../axios/axios";

class ModelService {
    #baseURL = "models";

    async getModelsWithYearsOfIssue(manufacturerId) {
        return axios.get(`${this.#baseURL}/withYears/${manufacturerId}`)
    }
}

export default new ModelService();