import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;

public class GUI {
    private JPanel mainPanel;
    private JPanel filePanel;
    private JPanel viewPanel;
    private JTree fileTree;
    private JButton openDirectoryButton;
    private JTextArea fileContentsArea;

    private GUI() {
        openDirectoryButton.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                super.mouseClicked(e);
                JFileChooser folderChooser = new JFileChooser();
                folderChooser.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
                folderChooser.setCurrentDirectory(new File("."));

                int folderReturnValue = folderChooser.showSaveDialog(mainPanel);
                if (folderReturnValue == JFileChooser.APPROVE_OPTION) {
                    File selectedFolder = folderChooser.getSelectedFile();
                    System.out.println(selectedFolder.getPath());
                    populateFileTree(selectedFolder);
                }
            }
        });
    }

    public static void main(String[] args) {
        JFrame mainFrame = new JFrame("FSV");
        mainFrame.setContentPane(new GUI().mainPanel);
        mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mainFrame.pack();
        mainFrame.setResizable(false);
        mainFrame.setMinimumSize(new Dimension(600, 400));
        mainFrame.setMaximumSize(new Dimension(600, 400));
        mainFrame.setVisible(true);
    }

    private void populateFileTree(File selectedFolder) {
        // TODO: Implement the population of the file tree
    }
}
