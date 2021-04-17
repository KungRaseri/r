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
            this.getHistory = { color: "red", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };
            this.getHistory = { color: "blue", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };
            this.getHistory = { color: "green", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };
            this.getHistory = { color: "purple", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };
            this.getHistory = { color: "black", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };
            this.getHistory = { color: "orange", message: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Molestie nunc non blandit massa enim nec. Purus in mollis nunc sed id semper risus in. Molestie ac feugiat sed lectus vestibulum mattis ullamcorper. In massa tempor nec feugiat nisl pretium." };

            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "ADD_MESSAGE":
                        this.getHistory = e.data.messageData;
                        break;
                    default:
                        break;
                }
            });

            console.log("MessageBox mounted.");
        }

        updated() {
            this.$el.scrollTop = this.$el.scrollHeight;
            console.log(this.$el.scrollHeight);
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
    .message {
        overflow: hidden;
        height: 30vh;
        width: 25vw;
    }
</style>