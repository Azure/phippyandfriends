/*global describe, it*/
'use strict';
const superagent = require('supertest');
const app = require('../app');
const request = superagent(app.listen());

describe('Routes', () => {
  describe('GET /', () => {
    it('should return 200', done => {
      request
        .get('/')
        .expect(200, done);
    });
  });
  describe('GET /messages', () => {
    it('should return 200', done => {
      request
        .get('/messages')
        .expect('Content-Type', /json/)
        .expect(200, done);
    });
  });
  describe('GET /messages/notfound', () => {
    it('should return 404', done => {
      request
        .get('/messages/notfound')
        .expect(404, done);
    });
  });
});
