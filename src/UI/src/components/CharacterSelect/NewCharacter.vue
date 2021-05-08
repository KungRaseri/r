<template>
    <v-fade-transition>
        <validation-observer v-slot="{ invalid }">
            <v-container v-show="Show" class="panel">
                <v-card class="card" rounded="xl" dark>
                    <h1>Character Info</h1>
                    <v-divider class="my-4" />
                    <v-container>
                        <v-row>
                            <v-col>
                                <validation-provider v-slot="{ errors }" rules="required">
                                    <v-text-field label="First Name" v-model="first" :color="ErrorColor(errors)" />
                                </validation-provider>
                            </v-col>
                            <v-col>
                                <validation-provider v-slot="{ errors }" rules="required">
                                    <v-text-field label="Last Name" v-model="last" :color="ErrorColor(errors)" />
                                </validation-provider>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-radio-group class="center" row mandatory label="Gender" v-model="gender">
                                    <v-radio v-slot:label value="male">
                                        <v-icon color="blue">mdi-gender-male</v-icon>
                                    </v-radio>
                                    <v-radio v-slot:label value="female">
                                        <v-icon color="pink">mdi-gender-female</v-icon>
                                    </v-radio>
                                    <v-radio v-slot:label value="nb">
                                        <v-icon color="yellow">mdi-gender-non-binary</v-icon>
                                    </v-radio>
                                </v-radio-group>
                            </v-col>
                            <v-col>
                                <v-menu ref="menu"
                                        :close-on-content-click="false"
                                        :return-value.sync="dob"
                                        transition="scale-transition"
                                        offset-y
                                        min-width="auto">
                                    <template v-slot:activator="{ on, attrs }">
                                        <validation-provider v-slot="{ errors }" :rules="{ required: true, regex: /^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$/ }">
                                            <v-text-field v-model="dob"
                                                          label="Date of Birth"
                                                          prepend-icon="mdi-calendar"
                                                          :color="ErrorColor(errors)"
                                                          v-bind="attrs"
                                                          v-on="on"></v-text-field>
                                        </validation-provider>
                                    </template>
                                    <v-date-picker v-model="dob"
                                                    no-title
                                                    scrollable>
                                        <v-spacer></v-spacer>
                                        <v-btn text
                                                color="primary"
                                                @click="menu = false">
                                            Cancel
                                        </v-btn>
                                        <v-btn text
                                                color="primary"
                                                @click="$refs.menu.save(dob)">
                                            OK
                                        </v-btn>
                                    </v-date-picker>
                                </v-menu>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-btn color="red" :rounded="true" block @click="goback()">Back</v-btn>
                            </v-col>
                            <v-col>
                                <v-btn color="green" :rounded="true" :disabled="invalid" @click="SaveData()" block>Save</v-btn>
                            </v-col>
                        </v-row>
                    </v-container>
                </v-card>
            </v-container>
        </validation-observer>
    </v-fade-transition>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Ref, Emit } from 'vue-property-decorator';
    import { extend, ValidationProvider, ValidationObserver } from 'vee-validate';
    import { required, regex } from 'vee-validate/dist/rules';

    extend('required', required)
    extend('regex', regex)

    @Component({
        components: {
            ValidationProvider,
            ValidationObserver
        },
    })

    export default class NewCharacter extends Vue {
        @Ref() readonly form!: any;
        @Prop(Boolean) show = false;

        valid = true;
        first = "";
        last = "";
        gender = "";
        dob = "";

        $axios: any;

        SaveData() {
            let FirstName = this.first;
            let LastName = this.last;
            let Gender = this.gender;
            let Dob = this.dob;
            this.$axios
                .post(
                    "http://framework/SAVE_NEW_CHARACTER",
                    {
                        FirstName,
                        LastName,
                        Gender,
                        Dob
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });

            this.saveform();
        }

        @Emit()
        goback() {
            return false;
        }

        @Emit()
        saveform() {
            return true;
        }

        ErrorColor(value: any[]) {
            if (value.length > 0) {
                return "red"
            }

            return "";
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }
    }
</script>

<style>
    .center .v-input--radio-group__input {
        justify-content: center;
    }
</style>

<style scoped>
    .panel {
        width: 35%;
        height: 100%;
        margin: auto;
        padding: 0;
    }

    .card {
        position: absolute;
        width: 35%;
        top: 50%;
        transform: translateY(-50%);
    }

    h1 {
        text-align: center;
    }
</style>