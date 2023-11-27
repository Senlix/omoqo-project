import { AxiosRequestConfig } from 'axios';
import apiClient from './apiClient';

export interface PaginatedList<T> {
  items: T[];
  page: number;
  limit: number;
  count: number;
}

class HttpService {
  endpoint: string;

  constructor(endpoint: string) {
    this.endpoint = endpoint;
  }

  getAll<T>(requestConfig?: AxiosRequestConfig) {
    const abortController = new AbortController();

    const request = apiClient.get<PaginatedList<T>>(this.endpoint, {
      signal: abortController.signal,
      ...requestConfig,
    });

    return { request, cancel: () => abortController.abort() };
  }

  getById<T>(id: number) {
    const abortController = new AbortController();

    const request = apiClient.get<T>(`${this.endpoint}/${id}`, {
      signal: abortController.signal,
    });

    return { request, cancel: () => abortController.abort() };
  }

  add(requestConfig?: AxiosRequestConfig) {
    const abortController = new AbortController();

    const request = apiClient.post(`${this.endpoint}`, {
      signal: abortController.signal,
      ...requestConfig,
    });

    return { request, cancel: () => abortController.abort() };
  }

  update(requestConfig?: AxiosRequestConfig) {
    const abortController = new AbortController();

    const request = apiClient.put(`${this.endpoint}`, {
      signal: abortController.signal,
      ...requestConfig,
    });

    return { request, cancel: () => abortController.abort() };
  }

  remove(id: number) {
    const abortController = new AbortController();

    const request = apiClient.delete(`${this.endpoint}/${id}`, {
      signal: abortController.signal,
    });

    return { request, cancel: () => abortController.abort() };
  }

  bulkRemove(requestConfig?: AxiosRequestConfig) {
    const abortController = new AbortController();

    const request = apiClient.delete(`${this.endpoint}`, {
      signal: abortController.signal,
      ...requestConfig,
    });

    return { request, cancel: () => abortController.abort() };
  }
}

const create = (endpoint: string) => new HttpService(endpoint);

export default create;
