<template>
    <div class="add-emp-container">
        <h2 class="form-title">Add Employee</h2>

        <form class="add-emp-form" enctype="multipart/form-data" @submit.prevent="submitForm">

            <div class="container-1">


                <label>Name</label>
                <input type="text" placeholder="Name" v-model="employee.name"
                       :class="this.nameErr ? 'errField' : ''"/>

                <label>Phone</label>
                <input type="text" placeholder="Phone" v-model="employee.phone" 
                       :class="this.phoneErr ? 'errField' : ''"/>

                <label>Email</label>
                <input type="email" placeholder="Email" v-model="employee.email" 
                       :class="this.emailErr ? 'errField' : ''"/>

                <label>Salary</label>
                <input type="number" placeholder="500" min="500" step="100" v-model="employee.salary"
                       :class="this.salaryErr ? 'errField' : ''"/>

                <label>Date Hired</label>
                <input type="date" placeholder="Date Hired" v-model="employee.dateHired" 
                       :class="this.dateErr ? 'errField' : ''"/>

                <div class="btn-msg">
                    <button type="submit">Add Employee</button>

                    <div class="success-msg-cont">
                        <h3>{{this.successMsg}}</h3>
                    </div>

                    <div class="err-msg-cont1">
                        <h3>{{this.errMsg}}</h3>
                    </div>

                </div>

            </div>

            <div class="container-2">

                <!-- <label>Skills</label>
     <input type="text" placeholder="Press Alt + , to add" v-model="tempSkill" @keyup="addSkill"/>
                <div v-for="skill in skills" :key="skill" class="skills-container">
                    {{ skill }}
                </div> -->

                <div class="file-container">

                    <label>Profile Picture</label>
                    <input type="file" accept=".png, .jpg" @change="handleFileChange($event)" />

                    <div class="image-preview">
                        <img :src="url" />
                    </div>

                </div>

            </div>

        </form>
    </div>
    

</template>

<script>
    export default {

        data() {
            return {
                tempSkill: "",
                skills: [],
                url: null,
                profilePic: null,

                employee: {
                    name: "",
                    phone: "",
                    email: "",
                    salary: "",
                    dateHired: "",
                    //image: ""
                },

                successMsg: "",
                errMsg: "",

                nameErr: false,
                emailErr: false,
                phoneErr: false,
                salaryErr: false,
                dateErr: false,

            }
        },


        methods: {
            async submitForm() {
                console.log("add employee submit clicked")

                this.successMsg = "";
                this.errMsg = "";

                this.nameErr = false;
                this.emailErr = false;
                this.phoneErr = false;
                this.salaryErr = false;
                this.dateErr = false;

                // client validate form
                if (this.employee.name == "")
                    this.nameErr = true;
                if (this.employee.email == "")
                    this.emailErr = true;
                if (this.employee.phone == "")
                    this.phoneErr = true;
                if (this.employee.salary == "")
                    this.salaryErr = true;
                if (this.employee.dateHired == "")
                    this.dateErr = true;

                // and for the image

                if (this.nameErr || this.phoneErr || this.emailErr || this.salaryErr || this.dateErr) { 
                    this.errMsg = "Please fill in the required fields.";
                    return;
                }

                if (this.url == null) {
                    this.errMsg = "Please select a file.";
                    return;
                }

               


                var dateString = this.employee.dateHired
                // why does it work now
               

                let formData = new FormData();

                formData.append('file', this.profilePic);


                formData.append('Name', this.employee.name);
                formData.append('Phone', this.employee.phone);
                formData.append('Email', this.employee.email);
                formData.append('Salary', this.employee.salary);
                formData.append('HiredDate', dateString);
                formData.append('IsVacationAllowed', true);


                const settings = {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`

                    },
                    body: formData
                }

                try {
                    const response = await fetch('http://localhost:5242/api/employee', settings);
                    const data = await response.json();

                    if (data.message) {
                        this.successMsg = data.message;
                    }

                    console.log(data);
                } catch (error) {
                    console.log(error.message);
                }
            },


            addSkill(e) {
                if (e.key === ',' && this.tempSkill) {
                    this.skills.push(this.tempSkill)
                    this.tempSkill = ''
                }
            },

            handleFileChange(e) {
                console.log("file change");
                const file = e.target.files[0];
                this.url = URL.createObjectURL(file); 

                this.profilePic = e.target.files[0];
            },
        },

    }
</script>

<style>

    .add-emp-container {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
    }

    .add-emp-container h2 {
        margin: auto;
        margin-top: 20px;
    }
    
    .add-emp-form {
        margin-top: 50px;
        display: flex;
        flex-direction: column;
        background-color: white;
        display: flex;
        flex-direction: row;
        margin-left: 155px;
    }

    .container-1 {
        display: flex;
        flex-direction: column;
        margin-right: 50px;
        width: 50%;
        background-color: white;
    }

    .container-2 {
        display: flex;
        flex-direction: column;
        width: 50%;
        background-color: white;
    }

    .container-1 button {
        margin-top: 0px;
        border-radius: 0px;
        padding-top: 0px;
        padding-bottom: 0px;
        height: 60px;
    }

    .add-emp-form label {
        margin-bottom: 10px;
        background-color: white;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .add-emp-form input {
        width: 50%;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
        margin-bottom: 20px;
    }

    .skills-container {
        background-color: white;
    }

    .file-container {
        background-color: white;
        display: flex;
        flex-direction: column;
    }

    .image-preview img {
        max-width: 30%;
        max-height: 500px;
        background-color: white;
    }

    input::file-selector-button {
        border-style: none;
        padding: 15px;
        width: 100px;
        cursor: pointer;
        font-weight: bold;
        background-color: #349eeb;
        color: white;
        text-indent: -3px;
    }

    input::file-selector-button:hover {
        background-color: #1b5885;
    }

    

    .add-emp-form input[type=file] {
        border-style: none;
    }


    .btn-msg {
        display: flex;
        align-items: center;
        gap: 15px;
    }


    .err-msg-cont1 h3 {
        color: #f54245;
    }

   .file-container input {
       border-style: solid;
   }

       
</style>