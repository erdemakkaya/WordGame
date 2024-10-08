import Requests from "./base/baseservice";

class WordService {
    async getAll() {
      let result = await Requests.get('word');
      return result;
    }
  
    async getAllByStatistic() {
      let result = await Requests.get('word/statistic');
      return result;
    }
    async get(id) {
      debugger;
      let result = await Requests.get(`word/${id}`);
      return result;
    }
  
    async getWordValues(id) {
      const result = await Requests.get(`word/${id}/values`);
      return result;
    }
  
    async createOrEdit(entity) {
      const wordResponse = { word: entity };
      const url = entity.id ? `word/${entity.id}` : `word`;
      let result = entity.id ? await Requests.put(url, wordResponse) : 
                        await Requests.post(url, wordResponse);
      return result;
    }

    async delete(id) {
      let result = await Requests.del(`word/${id}`);
      return result;
    }

    async true(id) {
      let result = await Requests.put(`word/true/${id}`);
      return result;
    }

    async false(id) {
      let result = await Requests.put(`word/false/${id}`);
      return result;
    }

    async getWordValues(id) {
      const result = await Requests.put(`word/${id}/values`);
      return result;
    }
  }
  
  export default new WordService();