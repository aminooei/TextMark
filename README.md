# TextMark

#### Steps to run the code on any machine:

* pull the repo to a local directory called TextMark.
* go to TextMark directory.
* run `docker image build -t bolbol .` to create an image called `bolbol`
* run `docker run -it -p 80:80 -p 443:443 --name text-mark bolbol` to start a container from the above image called `text-mark`
* now open your browser and go to `localhost:80`
