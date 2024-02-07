<template>
    <div class="modal-overlay" @click="$emit('close-modal')">
        <div class="update-emp-modal" @click.stop>
            <h2 class="form-title">Update Employee</h2>

            <form class="update-emp-form" @submit.prevent="submitForm">
                <div class="container-1a">

                    <label>Name</label>
                    <input type="text" placeholder="Name" v-model="employee.name" />

                    <label>Phone</label>
                    <input type="text" placeholder="Phone" v-model="employee.phone" />

                    <label>Email</label>
                    <input type="email" placeholder="Email" v-model="employee.email" />

                    <label>Salary</label>
                    <input type="number" placeholder="500" min="500" step="100" v-model="employee.salary" />

                    <div class="btn-msg2">
                        <button type="submit">Update</button>

                        <div class="success-msg-cont">
                            <h3>{{this.successMsg}}</h3>
                        </div>

                        <div class="err-msg-cont1">
                            <h3>{{this.errMsg}}</h3>
                        </div>

                    </div>

                </div>

                <div class="container-2a">

                    <!--<label>Date</label>
                      <input type="date" placeholder="Date Hired" id="update-emp-date" v-model="employee.dateHired"/>

                     -->
                    <!--<div class="skills-parent-container">

                    <label>Skills</label>
                    <input type="text" placeholder="Press Alt + , to add a skill" v-model="tempSkill" @keyup="addSkill" />
                          <div v-for="skill in skills" :key="skill" class="skills-container">
                              {{ skill }}
                          </div>
                      </div>
                      -->
                    <!-- Update vacation perms -->
                    <div class="perm-container">
                        <div class="perm-label">
                            <label>Vacation Permission</label>
                        </div>

                        <fieldset id="perm-group">
                            <div class="radio-group">
                                <input type="radio" name="perm-group" v-model="employee.vacationPerm" value="true" />
                                <label>Yes</label>
                            </div>

                            <div class="radio-group">
                                <input type="radio" name="perm-group" v-model="employee.vacationPerm" value="false" />
                                <label>No</label>
                            </div>

                        </fieldset>
                    </div>

                    <div class="file-container2">

                        <label>Profile Picture</label>
                        <input type="file" accept=".png, .jpg" @change="handleFileChange($event)" />

                        <div class="image-preview2">
                            <img :src="url" />
                        </div>

                    </div>

                </div>
            </form>
            <button class="exit-btn" type="button" @click="$emit('close-modal')" id="upd-exit-btn">Exit</button>
        </div>
    </div>
</template>


<script>
    export default {

        props: ['empID', 'employee', 'image'],

        data() {
            return {
                tempSkill: "",
                skills: [],
                url: this.image,

                profilePic: null,

                errMsg: "",
                successMsg: "",

            }
        },

        // ?? i dont get it help
        watch: {
            image(newImage) {
                this.url = newImage
            }
        },

        methods: {



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

            async submitForm() {

                console.log("submit for update employee is clicked");

                this.successMsg = "";
                var booleanPerm = this.employee.vacationPerm == "true" ? true : false;


                let formData = new FormData();

                if (this.profilePic != null) {
                    formData.append('file', this.profilePic);
                } else {
                    // add the original profile picture

                }

                formData.append('Name', this.employee.name);
                formData.append('Phone', this.employee.phone);
                formData.append('Email', this.employee.email);
                formData.append('Salary', this.employee.salary);
                formData.append('IsVacationAllowed', booleanPerm);


                const settings = {
                    method: 'PUT',
                    headers: {
                        'Accept': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    },
                    body: formData
                }

                try {
                    const response = await fetch('http://localhost:5242/api/employee/' + this.empID, settings);
                    const data = await response.json();

                    if (data.message != "" || data.message != null)
                        this.successMsg = data.message;

                    console.log(data);

                } catch (error) {
                    console.log(error.message);
                }
            },



            created() {
                console.log(this.employee);
            },

        },


    }
</script>

<style>

    .modal-overlay {
        position: fixed;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        display: flex;
        justify-content: center;
        background-color: #000000da;
    }

    .update-emp-modal {
        text-align: center;
        background-color: white;
        height: 650px;
        width: 800px;
        margin-top: 1%;
        margin-left: 15%;
        padding: 60px 0;
        border-radius: 20px;
    }

        .update-emp-modal button {
            margin-top: 0px;
        }

    .update-emp-form {
        height: 400px;
        width: 700px;
        display: flex;
        flex-direction: row;
        margin: auto;
        margin-left: 100px;
        text-align: left;
    }

    .container-1a {
        display: flex;
        flex-direction: column;
        width: 50%;
        margin-top: 30px;
    }

    .container-2a {
        display: flex;
        flex-direction: column;
        width: 50%;
        margin-top: 30px;
    }

    .update-emp-form label {
        margin-bottom: 10px;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .update-emp-form input {
        width: 50%;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
        margin-bottom: 20px;
        background-color: white;
    }

    .perm-container {
        margin-bottom: 20px;
    }

    .perm-label {
        margin-bottom: 15px;
    }

    #perm-group {
        border-style: none;
        display: flex;
        flex-direction: row;
    }


    .radio-group {
        width: 20%;
    }

        .radio-group label {
            font-weight: 100;
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


    .update-emp-form input[type=file] {
        border-style: none;
        margin-right: 175px;
    }


    .image-preview2 {
        text-align: left;
    }

        .image-preview2 img {
            width: 150px;
        }


    #update-emp-date {
        padding: 15px;
    }


    #upd-exit-btn {
        color: black;
        background-color: white;
        border-style: solid;
        border-width: 2px;
        padding-top: 13px;
        padding-bottom: 13px;
        margin-top: 175px;
    }

        #upd-exit-btn:hover {
            background-color: #ebedee;
            color: black;
        }

    .skills-parent-container {
        display: flex;
        flex-direction: column;
        margin-bottom: 20px;
    }

    .btn-msg2 {
        display: flex;
        gap: 15px;
        align-items: center;
    }
</style>