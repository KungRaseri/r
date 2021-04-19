<template>
    <v-container class="message">
        <v-row
            v-for="(item, index) in getHistory"
            :key="index"
        >
            <Bubble
                :ColorFill="item.color"
                :MessageText="item.message" />
        </v-row>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';
    import Bubble from './Bubble.vue';

    @Component({
        components: {
            Bubble
        },
    })
    export default class MessageBox extends Vue {
        @Prop(Boolean) isTextFieldActive = false;

        history: any[] = [];

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "ADD_MESSAGE":
                        this.getHistory = e.data.messageData;
                        break;
                    default:
                        break;
                }
            });
        }

        updated() {
            this.$el.scrollTop = this.$el.scrollHeight;
            if (!this.isTextFieldActive) {
                this.showmessages();
                this.timeout();
            }
        }

        get getHistory() {
            return this.history;
        }

        set getHistory(value: any) {
            this.history.push(value);
        }

        @Emit()
        showmessages() {
            return true;
        }

        @Emit()
        timeout() {
            return false;
        }
    }
</script>

<style scoped>
</style>