# TextMark

TextMark is an open source text annotation tool. It provides annotation features for text classification, and token classification tasks (e.g. NER). So, you can create labeled data for sentiment analysis, named entity recognition, extractive text summarization and so on. Just create a project, upload data files and start annotating. You can build a dataset in hours.

### Docker Demo

You can download and try the annotation demo on Docker on your machine. To install it on your PC, follow the step in bellow: 

You need to install Git and Docker and then clone the repository before running the docker-compose:

```
git clone https://github.com/aminooei/TextMark
cd TextMark
```

To run this project in Docker, you need to edit the `TextMark/appsettings.json` file and uncomment the "Connection String for Docker".
To run this project locally on your PC IIS, you need to comment out "Connection String for Docker" and uncomment the "Connection String for PC" in that file.

```
docker-compose up
```
After running the above commands, open <http://localhost:8080/>.


You may also download and run this demo project locally on your PC in Microsoft Visual Studio 2019, OR just run it in IIS.




## Documentation

To read the user manual, first you need to run the project and login to the Admin section by using the below information: 
[Username: Admin] ---- [Password: 12345]. 


## Contribution

As with any software, TextMark is under continuous development. If you have requests for features, please file an issue describing your request. Also, if you want to see work towards a specific feature, feel free to contribute by working towards it. The standard procedure is to fork the repository, add a feature, fix a bug, then file a pull request that your changes are to be merged into the main branch and we will includ it in the next release.



## Citation

```tex
@misc{TextMark,
  title={{TextMark}: Text Annotation Tool for Human},
  url={https://github.com/aminooei/TextMark},
  note={Software available from https://github.com/aminooei/TextMark},
  author={
    Amin Minouei},
  year={2022},
}
```

## Contact

For help and feedback, please feel free to contact [the author](https://github.com/aminooei).
