const mongoose = require('mongoose');
var Schema = mongoose.Schema;
const user = require('../models/user.model.js');


//var User3 = mongoose.model('user', user);

const task = mongoose.Schema({
    _id: mongoose.Schema.Types.ObjectId,
    name: {type: String, required: true},
    status: {type: String, required: true} ,
    user : { type:Schema.Types.ObjectId, ref: 'User' }


 });

 module.exports = mongoose.model('task', task);
