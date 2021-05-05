<template>
    <v-card v-if="Show" class="panel" dark>
        <v-container class="fill">
            <v-row dense>
                <v-col>
                    <v-btn color="red" :rounded="true" block @click="goback()">Back</v-btn>
                </v-col>
                <v-col>
                    <v-btn color="green" :rounded="true" block>Continue</v-btn>
                </v-col>
            </v-row>
            <v-divider class="my-4" />
            <v-container class="scroll">
                <v-expansion-panels>
                    <StyleComponent v-if="IsFreemode()" name="FaceBlend" />
                    <StyleComponent v-if="IsFreemode()" name="SkinBlend" />
                    <StyleComponent v-for="item in Components" :key="item" :name="item" />
                </v-expansion-panels>
            </v-container>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';
    import StyleComponent from './StyleComponent.vue';

    @Component({
        components: {
            StyleComponent
        },
    })
    export default class PedCustomize extends Vue {
        @Prop(Boolean) show = this.Show;
        @Prop(String) ped = this.Ped;

        components = [""];
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "AGGREGATE_COMPONENTS":
                        this.Components = e.data.comps;
                        break;
                    default:
                        break;
                }
            });
        }

        @Emit()
        goback() {
            return true;
        }

        IsFreemode() {
            if (this.Ped === "FreemodeFemale01" || this.Ped === "FreemodeMale01") {
                return true;
            }

            return false;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get Components() {
            return this.components;
        }

        set Components(value: string[]) {
            this.components = value;
        }

        get Ped() {
            return this.ped;
        }

        set Ped(value: string) {
            this.ped = value;
        }
    }
</script>

<style scoped>
    .panel {
        position: fixed;
        top: 0;
        right: 0;
        width: 20%;
        height: 100%;
    }

    .fill {
        height: 100%;
    }

    .scroll {
        overflow: scroll;
        height: 87%;
    }
</style>