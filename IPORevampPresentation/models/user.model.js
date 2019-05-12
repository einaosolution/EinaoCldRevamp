const mongoose = require('mongoose');
const task = require('../models/task.model.js');
const Schema = mongoose.Schema;

const user = mongoose.Schema({
   _id: mongoose.Schema.Types.ObjectId,
   email: {type: String, required: true},
   firstName: {type: String, required: true},
   lastName: {type: String, required: true},
   middleName: {type: String, required: false},
   address: {type: String, required: true},
   city: {type: String, required: true},
   state: {type: String, required: true},
   program: {type: String, required: true},
   phoneNumber: {type: String, required: true},
   dob: {type: String, required: true},
   password: {type: String, required: true},
   role: {type: String, required: true},
   task : [{ type: Schema.Types.ObjectId, ref: 'task' }]
});

module.exports = mongoose.model('User', user);
