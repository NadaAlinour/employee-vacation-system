<template>
    <main>

        <form class="login-form" @submit.prevent="submitForm">

            <div class="login-title-container">
                <h1 class="form-title">Login</h1>
            </div>
            <!-- client-side validation error message -->
            <div class="error-msg" v-show="emailErr || passErr">
                <p>Please fill in all fields.</p>
            </div>

            <!-- server error message -->
            <!-- render if isValid is false -->
            <div class="error-msg" v-if="!isValid">
                <p>{{this.errMsg}}</p>
            </div>



            <input type="text"
                   placeholder="Email"
                   v-model="email"
                   :class="{loginErr: emailErr}" />




            <input type="password"
                   placeholder="Password"
                   v-model="password"
                   :class="{loginErr: passErr}" />

            <button class="login-btn" type="submit">
                <p>Login</p>
                <box-icon class="icon" name='log-in' color="white"></box-icon>
            </button>


        </form>

    </main>
</template>

<script>

    import 'boxicons';

    export default {

        data() {
            return {
                email: "",
                password: "",
                isValid: true, // server side error
                emailErr: false,
                passErr: false,

                errMsg: "",

                token: "",

            }
        },

        methods: {

            clientValidate() {
                //console.log("validate input fields");

                this.emailErr = false;
                this.passErr = false;

                // all fields are filled
                if (this.email === "") this.emailErr = true;
                if (this.password === "") this.passErr = true;

                //console.log(this.emailErr, this.passErr);

                return !this.emailErr && !this.passErr;
            },

            async fetchAdmin() {

                const settings = {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        email: this.email,
                        password: this.password
                    })
                };

                try {
                    const response = await fetch('http://localhost:5242/api/login', settings);
                    const data = await response.json();

                    if (data.message)
                        this.errMsg = data.message;

                    else this.token = data.token;
                    //console.log(this.token);


                } catch (error) {
                    console.log(error.message);
                }
            },

            async submitForm() {
                //console.log("login was clicked")
                this.isValid = true;
                this.errMsg = "";


                var validateClient = this.clientValidate();
                if (!validateClient) {
                    console.log("Request to server cannot be sent.");
                    return;
                }

                await this.fetchAdmin();

                if (this.errMsg != "") {
                    this.isValid = false;
                }

                else {
                    console.log("logging in..");

                    // push to localstorage
                    // have them in the same string ig
                    localStorage.setItem('user', this.email);
                    localStorage.setItem('token', this.token);

                    // update store
                    this.$store.commit('updateUser', this.email);
                    this.$store.commit('updateToken', this.token);


                    this.$router.push('/employees');
                }
            }

        }
    }
</script>

<style>



    .login-form {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        margin: auto;
        width: 32%;
        min-height: 350px;
        margin-top: 80px;
        row-gap: 20px;
        padding: 35px;
        border: 1px solid rgba(0,0,0,.2);
    }


        .login-form input {
            border: 1px solid rgba(0,0,0,.2);
            padding: 15px;
            width: 65%;
            font-size: 1rem;
        }

            .login-form input::placeholder {
                font-size: 1rem;
            }

    /* for input fields with errors */
    .loginErr {
        border-style: solid;
        border-color: rgba(255, 0, 0, 0.4);
    }

    .login-btn {
        width: 72%;
        margin-top: 7px;
        margin-bottom: 25px;
        padding: 15px;
        font-size: 1rem;
        display: flex;
        justify-content: space-between;
    }

    .login-btn p {
        color: white;

    }

    .error-msg {
        border-style: solid;
        border-color: #ff8282;
        background-color: rgba(255, 0, 0, 0.2);
        text-align: center;
        padding: 17px;
        border-width: 1px;
        width: 65%;
        margin: auto;
        font-size: 1rem;
    }
</style>
