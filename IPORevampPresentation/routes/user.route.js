const express = require('express');
const router = express.Router();
const mongoose = require('mongoose');
const bcrypt = require('bcrypt');
const User = require('../models/user.model');
const Task = require('../models/task.model');

const yelp = require('yelp-fusion');
const apiKey = '3A3-p9y0K_s7fRLRmy0nWQRiw24sdVzxTuO6dusuqdeW8proshqf3-87wmNC1TScg5gCX5qwMxh5wL8_tq5khNm1b8uvmjGYngQR8SEfOWjHj7temIy8EClAU7j3WXYx';


router.get('/GetUser', function(req, res) {
  var email = req.query.property1;

  User.find().exec(function(err, user2) {

    if(err) {
      return res.status(500).json({
         error: err
      });
   }
   else {

    res.status(200).json({
      user2
   });

   }


  })


})


router.get('/Googlemap', function(req, res) {



const searchRequest = {
  term:'Kennesaw State University',
  location: 'Georgia'
};

const client = yelp.client(apiKey);

client.search(searchRequest).then(response => {
  const firstResult = response.jsonBody.businesses[0];
  const prettyJson = JSON.stringify(firstResult, null, 4);

  res.status(200).json({
    firstResult
 });
 // console.log(prettyJson);
}).catch(e => {
  console.log(e);
});




})

router.get('/GetTask', function(req, res) {
  var email = req.query.property1;

  User.findOne({email:email}).exec(function(err, user2) {


    Task.find({user:user2._id}).exec(function(err, task) {
      if(err) {
        return res.status(500).json({
           error: err
        });
     }
     else {

      res.status(200).json({
        task
     });

     }



  })

  })


})

router.get('/Count', function(req, res) {
var email = req.query.property1;


User.findOne({email:email}).exec(function(err, user) {

  if(err) {
    return res.status(500).json({
       error: err
    });
 }
 else {


  if (user) {
    res.status(200).json({
      success: "User Already Exist"
   });
  }

  else {

    res.status(200).json({
      success:  'User Ok'
   });

  }

}

})



});
router.post('/signup', function(req, res) {
   bcrypt.hash(req.body.password, 10, function(err, hash){
      if(err) {
         return res.status(500).json({
            error: err
         });
      }
      else {
         const user = new User({
            _id: new  mongoose.Types.ObjectId(),
            email: req.body.email,
            role: req.body.role,
            firstName: req.body.firstName,
            lastName: req.body.lastName,
            middleName: req.body.middleName,
            address: req.body.address,
            city: req.body.city,
            state: req.body.state,
            program: req.body.program,
            phoneNumber: req.body.phoneNumber,
            dob: req.body.dob,
            password: hash
         });
         console.log("new user"  )
         console.log(user);
         user.save().then(function(result) {
            console.log(result);
            res.status(200).json({
               success: 'New user has been created'
            });
         }).catch(error => {
           console.log(error)
            res.status(500).json({
               error: err
            });
         });
      }
   });
});


router.post('/createtask', function(req, res) {

  var email = req.body.email;
  User.findOne({email:email}).exec(function(err, user) {
    if(err) {
      return res.status(500).json({
         error: err
      });
   }

   else {

    const task= new Task({
      _id: new  mongoose.Types.ObjectId(),
      name: req.body.name,
      status: req.body.status,
      user: user
   });

   task.save().then(function(result) {
    console.log(result);
    res.status(200).json({
       success: 'New Task  been created'
    });
 }).catch(error => {
    res.status(500).json({
       error: err
    });
 });

   }

  })

});

router.get('/deletetask', function(req, res) {


console.log(req.query.property1)

  Task.findOneAndDelete(req.params.property1, (err, todo) => {

    if (err) return res.status(500).send(err);

    const response = {
        message: "Todo successfully deleted"

    };
    return res.status(200).send(response);
});


});



router.post('/updatetask', function(req, res) {

  var id = req.body._id;

  Task.findByIdAndUpdate(

    req.body._id,

    req.body,

    {new: true},


    (err, todo) => {

        if (err)  return res.status(500).send(err);
        return res.send(todo);
    }
)


});

router.post('/signin', function(req, res){
  User.findOne({email: req.body.email})
  .exec()
  .then(function(user) {
     bcrypt.compare(req.body.password, user.password, function(err, result){
        if(err) {
           return res.status(401).json({
              failed: 'Unauthorized Access'
           });
        }
        if(result) {
           return res.status(200).json({
               user
           });
        }
        return res.status(401).json({
           failed: 'Unauthorized Access'
        });
     });
  })
  .catch(error => {
     res.status(500).json({
        error: error
     });
  });;
});

module.exports = router;
